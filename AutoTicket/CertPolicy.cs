using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AutoTicket
{
    public class CertPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint
            , X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            // 你可以在这里加上证书检验的方法，错误值可以在WinError.h中获得
            // 这里只是简单的返回true，任何证书都可以正常的使用。
            return true;
        }
    }
}
