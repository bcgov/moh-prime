using System;

namespace Prime.HttpClients.DocumentManagerApiDefinitions
{
    public static class DestinationFolders
    {
        public const string SignedAgreements = "signed_agreements";
        public const string SelfDeclarations = "self_declarations";
        public const string EnrolleeAdjudicationDocuments = "enrollee_adjudication_document";
        public const string SignedOrgAgreements = "signed_org_agreements";
        public const string BusinessLicences = "business_licences";
        public const string SiteAdjudicationDocuments = "site_adjudication_document";
    }

    public class DownloadToken
    {
        public string Token { get; set; }
    }

    public class DocumentResponse
    {
        public Guid Document_guid { get; set; }
    }
}
