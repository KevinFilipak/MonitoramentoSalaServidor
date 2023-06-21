

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_Report_CA003')
    DROP PROCEDURE sp_Report_CA003
GO

CREATE PROCEDURE sp_Report_CA003
(
     @conta_i_id INT  
    ,@data_inicial DATETIME
    ,@data_final DATETIME
)

AS
SET NOCOUNT OFF;

      

SET NOCOUNT ON;
GO
