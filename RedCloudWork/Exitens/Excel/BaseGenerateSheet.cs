using NPOI.SS.UserModel;

namespace RedCloudWork.Exitens.Excel
{
    public abstract class BaseGenerateSheet
    {
        public string SheetName { set; get; }

        public IWorkbook Workbook { get; set; }

        public virtual void GenSheet(ISheet sheet)
        {

        }
    }
}