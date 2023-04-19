using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaMonitoramento.Util.Servicos;
using X.PagedList;

namespace SistemaMonitoramento.Web.Models
{
    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTable
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public List<object> data { get; set; }

        public DataTable()
        {

        }

        public static DataTable GerarTabela<T>(DataTableAjaxPostModel dataTableAjaxPostModel, IEnumerable<T> list)
        {
            try
            {
                var _obj = new DataTable();

                _obj.recordsTotal = list.Count();

                _obj.data = list.Cast<object>().ToList().AsQueryable()
                    .OrderBy(dataTableAjaxPostModel.columns[dataTableAjaxPostModel.order[0].column].data + "|" + dataTableAjaxPostModel.order[0].dir)
                    .ToPagedList((dataTableAjaxPostModel.start + dataTableAjaxPostModel.length) / dataTableAjaxPostModel.length, dataTableAjaxPostModel.length).ToList();

                _obj.recordsFiltered = _obj.recordsTotal;
                _obj.draw = dataTableAjaxPostModel.draw;

                return _obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}