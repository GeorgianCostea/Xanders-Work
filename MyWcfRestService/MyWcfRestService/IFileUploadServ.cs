using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;

namespace MyWcfRestService
{
    [ServiceContract]
    public interface IFileUploadServ
    {
        [OperationContract]
        [WebGet(UriTemplate = "File/{fileName}/{fileExtension}")]
        Stream DownloadFile(string fileName, string fileExtension);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UploadFile?fileName={fileName}")]
        void UploadFile(string fileName, Stream stream);
    }
}
