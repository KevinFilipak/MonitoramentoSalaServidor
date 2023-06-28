#include <DHT.h>

#include <ESP8266WiFi.h>          // Biblioteca WIFI
#include <ESP8266HTTPClient.h>
#define DHTPIN D1                 // pino que estamos conectado
#define DHTTYPE DHT11             // DHT 11

DHT dht(DHTPIN, DHTTYPE);

const char* NOME_REDE = "#REDEWIFI"; //Nome da rede WiFi
const char* SENHA_REDE = "#IPWIFI"; //Senha da rede WiFi


//http://191.252.102.108/Monitoramento?Dispositivo=0&Temperatura=10&Umidade=10

String LINK = "http://191.252.102.108/Monitoramento?";
String RESPOSTA = "";
String ERRO = "";
String DISPOSITIVO = "#CODIGODISPOSITIVO";
WiFiClient client;
HTTPClient http;

float h = 0;
float t = 0;

void setup()
{
  Serial.begin(115200);
  Serial.println("DHTxx test!");
  dht.begin();

  //Inicializa a conexão WIFI
  InicializaWifi();
  delay(1000);
}

void loop()
{
  h = dht.readHumidity();
  t = dht.readTemperature();

  if (isnan(t) || isnan(h))
  {
    Serial.println("Failed to read from DHT");
  }
  else
  {
    Serial.print("Umidade: ");
    Serial.print(h);
    Serial.print("%t ");
    Serial.print("Temperatura: ");
    Serial.print(t);
    Serial.println(" *C");
    EnviarSinal();
    
  }
}

void EnviarSinal() {

  Serial.println(LINK + "Dispositivo=" + DISPOSITIVO + "&Temperatura=" + t + "&Umidade=" + h);
  http.begin(client, LINK + "Dispositivo=" + DISPOSITIVO + "&Temperatura=" + t + "&Umidade=" + h);

  int httpCode = http.GET();

  if (httpCode > 0) {
    if (httpCode == HTTP_CODE_OK) {
      RESPOSTA = http.getString();
      RESPOSTA.remove(0, 1);
      RESPOSTA.remove(RESPOSTA.length() - 1, 1);
      ERRO = "";
      Serial.println(RESPOSTA);
    }
  }
  else {
    ERRO = http.errorToString(httpCode).c_str();
    Serial.printf("[HTTP] GET... failed, error: %s\n", http.errorToString(httpCode).c_str());


  }
  delay(#DELAYSINAL); // delay 5 min = 300000
}

void InicializaWifi() {

  WiFi.mode(WIFI_STA);
  WiFi.begin(NOME_REDE, SENHA_REDE);

  WiFi.persistent(false);
  WiFi.disconnect();
  delay(1000);
  WiFi.mode(WIFI_AP);
  WiFi.begin(NOME_REDE, SENHA_REDE);

  int _tentativas = 0;


  while (WiFi.status() != WL_CONNECTED) {

    delay(1000);
    _tentativas++;


  }
}
