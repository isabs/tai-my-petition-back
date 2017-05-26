using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WcfJsonRestService.Model;
using WcfJsonRestService.WebModel;
using Person = WcfJsonRestService.WebModel.Person;

namespace WcfJsonRestService
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "petitions" )]
        List<PetitionShort> GetAllPetitions ( );

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "petition/{petitionId}" )]
        PetitionNormal GetPetition ( string petitionId );

        [OperationContract]
        [WebInvoke ( Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "petitions" )]
        int AddPetition ( RequestBody requestBody );

        [OperationContract]
        [WebInvoke ( Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/petitions/{petitionId}/sign" )]
        void SignPetition ( string petitionId );

        [OperationContract]
        [WebInvoke ( Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "/petitions/{petitionId}" )]
        void DeletePetition ( string petitionId );

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "/petitions?own=true" )]
        List<PetitionShort> GetPetitionForCurrentUser ( );

        #region not used

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "data/{id}" )]
        Person GetData ( string id );

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
                  UriTemplate = "addPerson/{name}" )]
        Result AddPerson ( string name );

        [OperationContract]
        [WebGet ( ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "sampleReqBody" )]
        RequestBody GetSampleRequestBody ( );

        #endregion
    }
}
