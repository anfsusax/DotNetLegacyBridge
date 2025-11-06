using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GTI.Wcf
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();
    }
}
