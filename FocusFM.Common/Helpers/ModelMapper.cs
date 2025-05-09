using Newtonsoft.Json;

namespace FocusFM.Common.Helpers
{
    public static class ModelMapper
    {
        //public static QuotesRequestModel MapToQuotesRequestModel(QuotesSaveRequestModel saveModel)
        //{
        //    // Initialize the QuotesRequestModel
        //    var requestModel = new QuotesRequestModel
        //    {
        //        QuotesId = saveModel.QuotesId,
        //        UserId = saveModel.UserId,
        //        CustomerName = saveModel.CustomerName,
        //        CompanyName = saveModel.CompanyName,
        //        CustomerEmail = saveModel.CustomerEmail,
        //        CustomerMobileNo = saveModel.CustomerMobileNo,
        //        //Margin = saveModel.Margin,
        //        Documents = saveModel.Documents,
        //        DocumentNames = saveModel.DocumentNames,
        //        ProjectBrief = saveModel.ProjectBrief
        //    };

        //    // Convert JSON strings to their respective lists
        //    if (!string.IsNullOrEmpty(saveModel.MainSelectedItems))
        //    {
        //        requestModel.MainSelectedItems = JsonConvert.DeserializeObject<List<MainQuotesRequestModel>>(saveModel.MainSelectedItems);
        //    }
        //    else
        //    {
        //        requestModel.MainSelectedItems = new List<MainQuotesRequestModel>();
        //    }

        //    if (!string.IsNullOrEmpty(saveModel.SubSelectedItems))
        //    {
        //        requestModel.SubSelectedItems = JsonConvert.DeserializeObject<List<SubQuotesRequestModel>>(saveModel.SubSelectedItems);
        //    }
        //    else
        //    {
        //        requestModel.SubSelectedItems = new List<SubQuotesRequestModel>();
        //    }

        //    if (!string.IsNullOrEmpty(saveModel.DomainItems))
        //    {
        //        requestModel.DomainItems = JsonConvert.DeserializeObject<List<DomainRequestModel>>(saveModel.DomainItems);
        //    }
        //    else
        //    {
        //        requestModel.DomainItems = new List<DomainRequestModel>();
        //    }

        //    return requestModel;
        //}

        //public static SalesRequestFormSaveModel MapToSalesRequestFormModel(SalesRequestFormModel saveModel)
        //{
        //    // Initialize the SalesRequestFormSaveModel
        //    var requestModel = new SalesRequestFormSaveModel
        //    {
        //        RequestFormId = saveModel.RequestFormId,
        //        SalesId = saveModel.SalesId,
        //        UserId = saveModel.UserId
        //    };

        //    // Convert JSON strings to their respective lists

        //    if (!string.IsNullOrEmpty(saveModel.QuoteFormQuestions))
        //    {
        //        requestModel.QuoteFormQuestions = JsonConvert.DeserializeObject<List<QuoteFormQuestion>>(saveModel.QuoteFormQuestions);
        //    }
        //    else
        //    {
        //        requestModel.QuoteFormQuestions = new List<QuoteFormQuestion>();
        //    }

        //    if (!string.IsNullOrEmpty(saveModel.QuoteFormOptions))
        //    {
        //        requestModel.QuoteFormOptions = JsonConvert.DeserializeObject<List<QuoteFormOption>>(saveModel.QuoteFormOptions);
        //    }
        //    else
        //    {
        //        requestModel.QuoteFormOptions = new List<QuoteFormOption>();
        //    }

        //    return requestModel;
        //}
    }
}