using ApiConnectOracle.ExchangeService;
using ApiConnectOracle.Helper;
using ApiConnectOracle.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;

namespace ApiConnectOracle.Controllers
{
    [RoutePrefix("api/ExchangeService")]
    public class SendMailTripLeaveController : ApiController
    {
         
        string WCFUser = ConfigurationManager.AppSettings["WCFUser"];
        string WCFPass = ConfigurationManager.AppSettings["WCFPass"];
        ServiceClient serviceClient = new ExchangeService.ServiceClient();
        MailTripDataSet mMailTripDs = new MailTripDataSet();
        /// <summary>
        /// Danh sách test.
        /// </summary>
        [HttpGet]
        [Route("TestList")]
        public async Task<IHttpActionResult> TestList()
        {
            // await Task.Delay(1000);

            int v_MaBcDong = 101000;//703000
            int v_MaBcNhan = 100991;
            int v_LoaiChuyenThu = 0;
            string v_LoaiDichVu = "";
            int v_Ngay = 20170401;
            int v_SoChuyenThu = 204;
            // data test getlistMailtrip 
            int v_MaBcDong1 = 122160;//703000
            int v_MaBcNhan1 = 100991;
            int v_LoaiChuyenThu1 = 0;
            string v_LoaiDichVu1 = "";
            int v_Ngay1 = 20140310;
            int v_SoChuyenThu1 = 204;

           // var templistmailtrip = E1I2DA.GetListMailTrip_1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay1, v_SoChuyenThu);

            Load_Item_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay1, v_SoChuyenThu);
            var data = mMailTripDs.Tables["Item"];
            string json = DataTableToJSON(data);
            string json1 = JsonConvert.SerializeObject(data, Formatting.Indented);
            List<DataRow> list = data.AsEnumerable().ToList();
            
            //string serviceReturn = string.Empty;
            //serviceReturn = serviceClient.AddMailtrip(mMailTripDs, WCFUser, WCFPass);
            return Ok(new ResponseCode { code ="success",message= "get list item from table E1I2"});
        }


        private void Load_Item_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtItem = new DataTable();
            E1I2 myE1I2 = new E1I2();
            mMailTripDs.Tables["Item"].Clear();

            //Add vao table Item
            dtItem = myE1I2.GetListItem(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
            foreach (DataRow dr in dtItem.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Item"].NewRow();
                    #region
                    newrow["ItemCode"] = dr["ItemCode"];
                    newrow["AcceptancePOSCode"] = dr["AcceptancePOSCode"] is System.DBNull ? "" : dr["AcceptancePOSCode"];
                    newrow["SenderFullname"] = dr["SenderFullname"] is System.DBNull ? "" : dr["SenderFullname"];
                    newrow["SenderAddress"] = dr["SenderAddress"] is System.DBNull ? "" : dr["SenderAddress"];
                    newrow["CustomerCode"] = dr["CustomerCode"] is System.DBNull ? "" : dr["CustomerCode"];
                    newrow["BatchCode"] = dr["BatchCode"] is System.DBNull ? "" : dr["BatchCode"];
                    newrow["ReceiverFullname"] = dr["ReceiverFullname"] is System.DBNull ? "" : dr["ReceiverFullname"];
                    newrow["ReceiverTel"] = dr["ReceiverTel"] is System.DBNull ? "" : dr["ReceiverTel"];
                    newrow["ReceiverAddress"] = dr["ReceiverAddress"] is System.DBNull ? "" : dr["ReceiverAddress"];
                    newrow["isDomestic"] = dr["isDomestic"] is System.DBNull ? 0 : dr["isDomestic"];
                    newrow["CountryCode"] = dr["CountryCode"] is System.DBNull ? "" : dr["CountryCode"];
                    newrow["POSCode"] = dr["POSCode"] is System.DBNull ? "" : dr["POSCode"];
                    newrow["IsStatePrice"] = dr["IsStatePrice"] is System.DBNull ? 0 : dr["IsStatePrice"];
                    newrow["StatePriceValue"] = dr["StatePriceValue"] is System.DBNull ? 0 : dr["StatePriceValue"];
                    newrow["SendingContent"] = dr["SendingContent"] is System.DBNull ? "" : dr["SendingContent"];
                    newrow["Note"] = dr["Note"] is System.DBNull ? "" : dr["Note"];
                    newrow["ItemTypeCode"] = dr["ItemTypeCode"] is System.DBNull ? "" : dr["ItemTypeCode"];
                    newrow["SendingTime"] = dr["SendingTime"] is System.DBNull ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["SendingTime"]));
                    newrow["IsAirmail"] = dr["IsAirmail"] is System.DBNull ? 1 : dr["IsAirmail"];
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? 0 : dr["Weight"];
                    newrow["Status"] = dr["Status"] is System.DBNull ? 0 : dr["Status"];
                    newrow["TotalFreight"] = dr["TotalFreight"] is System.DBNull ? 0 : dr["TotalFreight"];
                    newrow["EmployeeCode"] = dr["EmployeeCode"] is System.DBNull ? "" : dr["EmployeeCode"];
                    newrow["SenderJob"] = dr["SenderJob"] is System.DBNull ? "" : dr["SenderJob"];
                    newrow["ProvinceCode"] = dr["ProvinceCode"] is System.DBNull ? "" : dr["ProvinceCode"];
                    newrow["LightItem"] = dr["LightItem"] is System.DBNull ? 0 : dr["LightItem"];
                    newrow["SectionCode"] = dr["SectionCode"] is System.DBNull ? "" : dr["SectionCode"];
                    newrow["ReceiverJob"] = dr["ReceiverJob"] is System.DBNull ? "" : dr["ReceiverJob"];
                    newrow["IsOpened"] = dr["IsOpened"] is System.DBNull ? 0 : dr["IsOpened"];
                    newrow["CertificateNumber"] = dr["CertificateNumber"] is System.DBNull ? "" : dr["CertificateNumber"];
                    newrow["LicenseNumber"] = dr["LicenseNumber"] is System.DBNull ? "" : dr["LicenseNumber"];
                    newrow["InvoiceNumber"] = dr["InvoiceNumber"] is System.DBNull ? "" : dr["InvoiceNumber"];
                    newrow["SenderIdentification"] = dr["SenderIdentification"] is System.DBNull ? "" : dr["SenderIdentification"];
                    newrow["SenderIssueDate"] = dr["SenderIssueDate"] is System.DBNull ? dr["SenderIssueDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["SenderIssueDate"]));
                    newrow["SenderIssueCountry"] = dr["SenderIssueCountry"] is System.DBNull ? "" : dr["SenderIssueCountry"];
                    newrow["ReceiverIdentification"] = dr["ReceiverIdentification"] is System.DBNull ? "" : dr["ReceiverIdentification"];
                    newrow["ReceiverIssueDate"] = dr["ReceiverIssueDate"] is System.DBNull ? dr["ReceiverIssueDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["ReceiverIssueDate"]));
                    newrow["ReceiverIssueCountry"] = dr["ReceiverIssueCountry"] is System.DBNull ? "" : dr["ReceiverIssueCountry"];
                    newrow["SenderTel"] = dr["SenderTel"] is System.DBNull ? "" : dr["SenderTel"];
                    newrow["MainFreight"] = dr["MainFreight"] is System.DBNull ? 0 : dr["MainFreight"];
                    newrow["VATFreight"] = dr["VATFreight"] is System.DBNull ? 0 : dr["VATFreight"];
                    newrow["SubFreight"] = dr["SubFreight"] is System.DBNull ? 0 : dr["SubFreight"];
                    newrow["IsPostFree"] = dr["IsPostFree"] is System.DBNull ? 0 : dr["IsPostFree"];
                    newrow["StatePriceFreight"] = dr["StatePriceFreight"] is System.DBNull ? 0 : dr["StatePriceFreight"];
                    newrow["PrintedNumber"] = dr["PrintedNumber"] is System.DBNull ? 1 : dr["PrintedNumber"];
                    newrow["DataCode"] = dr["DataCode"] is System.DBNull ? "" : dr["DataCode"];
                    newrow["SenderCustomReference"] = dr["SenderCustomReference"] is System.DBNull ? "" : dr["SenderCustomReference"];
                    newrow["RemainingFreight"] = dr["RemainingFreight"] is System.DBNull ? 0 : dr["RemainingFreight"];
                    newrow["ReceiverCustomReference"] = dr["ReceiverCustomReference"] is System.DBNull ? "" : dr["ReceiverCustomReference"];
                    newrow["IsReturn"] = dr["IsReturn"] is System.DBNull ? 0 : dr["IsReturn"];
                    newrow["IsCompensate"] = dr["IsCompensate"] is System.DBNull ? 0 : dr["IsCompensate"];
                    newrow["IsForward"] = dr["IsForward"] is System.DBNull ? 0 : dr["IsForward"];
                    newrow["IsAirmailForward"] = dr["IsAirmailForward"] is System.DBNull ? 0 : dr["IsAirmailForward"];
                    newrow["IsAirmailReturn"] = dr["IsAirmailReturn"] is System.DBNull ? 0 : dr["IsAirmailReturn"];
                    newrow["IsDebt"] = dr["IsDebt"] is System.DBNull ? 0 : dr["IsDebt"];
                    newrow["MachineName"] = dr["MachineName"] is System.DBNull ? "" : dr["MachineName"];
                    newrow["AcceptedIndex"] = dr["AcceptedIndex"] is System.DBNull ? 1 : dr["AcceptedIndex"];
                    newrow["BC16Index"] = dr["BC16Index"] is System.DBNull ? 1 : dr["BC16Index"];
                    newrow["IncomingIndex"] = dr["IncomingIndex"] is System.DBNull ? 1 : dr["IncomingIndex"];
                    newrow["ServiceCode"] = dr["ServiceCode"] is System.DBNull ? "E" : dr["ServiceCode"];
                    newrow["ReceiverDistrictCode"] = dr["ReceiverDistrictCode"] is System.DBNull ? "" : dr["ReceiverDistrictCode"].ToString();
                    newrow["LetterMoneyOrderFreight"] = dr["LetterMoneyOrderFreight"] is System.DBNull ? 0 : dr["LetterMoneyOrderFreight"];
                    newrow["ValueAddedServiceFreightTotalFreight"] = dr["VASFTF"];
                    newrow["OrderCode"] = dr["OrderCode"] is System.DBNull ? "" : dr["OrderCode"];
                    newrow["ReceiverAddressCode"] = dr["ReceiverAddressCode"] is System.DBNull ? "" : dr["ReceiverAddressCode"].ToString();
                    newrow["SenderMobile"] = dr["SenderMobile"] is System.DBNull ? "" : dr["SenderMobile"];
                    newrow["SenderFax"] = dr["SenderFax"] is System.DBNull ? "" : dr["SenderFax"];
                    newrow["SenderEmail"] = dr["SenderEmail"] is System.DBNull ? "" : dr["SenderEmail"];
                    newrow["ReceiverMobile"] = dr["ReceiverMobile"] is System.DBNull ? "" : dr["ReceiverMobile"];
                    newrow["ReceiverFax"] = dr["ReceiverFax"] is System.DBNull ? "" : dr["ReceiverFax"];
                    newrow["ReceiverEmail"] = dr["ReceiverEmail"] is System.DBNull ? "" : dr["ReceiverEmail"];
                    newrow["Discount"] = dr["Discount"] is System.DBNull ? 0 : dr["Discount"];
                    newrow["Abatement"] = dr["Abatement"] is System.DBNull ? 0 : dr["Abatement"];
                    newrow["UndeliverableGuide"] = dr["UndeliverableGuide"] is System.DBNull ? 1 : dr["UndeliverableGuide"];
                    newrow["Width"] = dr["Width"] is System.DBNull ? 0 : dr["Width"];
                    newrow["Height"] = dr["Height"] is System.DBNull ? 0 : dr["Height"];
                    newrow["Length"] = dr["Length"] is System.DBNull ? 0 : dr["Length"];
                    newrow["CheckSum"] = dr["CheckSum"] is System.DBNull ? "" : dr["CheckSum"];
                    newrow["ItemNumber"] = dr["ItemNumber"] is System.DBNull ? "" : dr["ItemNumber"];
                    newrow["ExchangeRateCode"] = dr["ExchangeRateCode"] is System.DBNull ? "" : dr["ExchangeRateCode"];
                    newrow["CODAddress"] = dr["CODAddress"] is System.DBNull ? "" : dr["CODAddress"];
                    newrow["CODPayment"] = dr["CODPayment"] is System.DBNull ? 0 : dr["CODPayment"];
                    newrow["SenderDistrictCode"] = dr["SenderDistrictCode"] is System.DBNull ? "" : dr["SenderDistrictCode"];
                    newrow["ReceiverContact"] = dr["ReceiverContact"] is System.DBNull ? "" : dr["ReceiverContact"];
                    newrow["UndeliverableReason"] = dr["UndeliverableReason"] is System.DBNull ? "" : dr["UndeliverableReason"];
                    newrow["DecisionNo"] = dr["DecisionNo"] is System.DBNull ? "" : dr["DecisionNo"];
                    newrow["DecisionDate"] = dr["DecisionDate"] is System.DBNull ? dr["DecisionDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["DecisionDate"]));
                    newrow["ReturnDayNumber"] = dr["ReturnDayNumber"] is System.DBNull ? 0 : dr["ReturnDayNumber"];
                    newrow["DiscountPercentage"] = dr["DiscountPercentage"] is System.DBNull ? 0 : dr["DiscountPercentage"];
                    newrow["DiscountAmount"] = dr["DiscountAmount"] is System.DBNull ? 0 : dr["DiscountAmount"];
                    newrow["ExecuteOrder"] = dr["ExecuteOrder"] is System.DBNull ? "" : dr["ExecuteOrder"];
                    newrow["InvoiceAttached"] = dr["InvoiceAttached"] is System.DBNull ? 0 : dr["InvoiceAttached"];
                    newrow["OtherAttached"] = dr["OtherAttached"] is System.DBNull ? 0 : dr["OtherAttached"];
                    newrow["OtherAttachedInfor"] = dr["OtherAttachedInfor"] is System.DBNull ? "" : dr["OtherAttachedInfor"];
                    newrow["TransferMachine"] = dr["TransferMachine"] is System.DBNull ? "" : dr["TransferMachine"];
                    newrow["TransferUser"] = dr["TransferUser"] is System.DBNull ? "" : dr["TransferUser"];
                    newrow["TransferPOSCode"] = dr["TransferPOSCode"] is System.DBNull ? "" : dr["TransferPOSCode"];
                    newrow["TransferDate"] = dr["TransferDate"] is System.DBNull ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransferDate"]));
                    newrow["TransferStatus"] = dr["TransferStatus"] is System.DBNull ? 0 : dr["TransferStatus"];
                    newrow["TransferTimes"] = dr["TransferTimes"] is System.DBNull ? 0 : dr["TransferTimes"];
                    newrow["WeightConvert"] = dr["WeightConvert"] is System.DBNull ? 0 : dr["WeightConvert"];
                    newrow["IsDiscount"] = dr["IsDiscount"] is System.DBNull ? 0 : dr["IsDiscount"];
                    newrow["InvoiceExport"] = dr["InvoiceExport"] is System.DBNull ? 0 : dr["InvoiceExport"];
                    newrow["Total"] = dr["Total"] is System.DBNull ? 0 : dr["Total"];
                    newrow["AcceptedType"] = dr["AcceptedType"] is System.DBNull ? 0 : dr["AcceptedType"];
                    newrow["SenderTaxCode"] = dr["SenderTaxCode"] is System.DBNull ? "" : dr["SenderTaxCode"];
                    newrow["VATPercentage"] = dr["VATPercentage"] is System.DBNull ? 0 : dr["VATPercentage"];
                    newrow["FuelSurchargeFreight"] = dr["FuelSurchargeFreight"] is System.DBNull ? 0 : dr["FuelSurchargeFreight"];
                    newrow["FarRegionFreight"] = dr["FarRegionFreight"] is System.DBNull ? 0 : dr["FarRegionFreight"];
                    newrow["AirSurchargeFreight"] = dr["AirSurchargeFreight"] is System.DBNull ? 0 : dr["AirSurchargeFreight"];
                    newrow["OtherFreight"] = dr["OtherFreight"] is System.DBNull ? 0 : dr["OtherFreight"];
                    newrow["TotalFreightVAT"] = dr["TotalFreightVAT"] is System.DBNull ? 0 : dr["TotalFreightVAT"];
                    newrow["TotalFreightDiscount"] = dr["TotalFreightDiscount"] is System.DBNull ? 0 : dr["TotalFreightDiscount"];
                    newrow["TotalFreightDiscountVAT"] = dr["TotalFreightDiscountVAT"] is System.DBNull ? 0 : dr["TotalFreightDiscountVAT"];
                    newrow["ReceiverTaxCode"] = dr["ReceiverTaxCode"] is System.DBNull ? "" : dr["ReceiverTaxCode"];
                    newrow["FarRegion"] = dr["FarRegion"] is System.DBNull ? 0 : dr["FarRegion"];
                    newrow["IsCollection"] = dr["IsCollection"] is System.DBNull ? 0 : dr["IsCollection"];
                    newrow["CustomerAccountNo"] = dr["CustomerAccountNo"] is System.DBNull ? "" : dr["CustomerAccountNo"];
                    newrow["IsFeedback"] = dr["IsFeedback"] is System.DBNull ? 0 : dr["IsFeedback"];
                    newrow["FeedbackPercentage"] = dr["FeedbackPercentage"] is System.DBNull ? 0 : dr["FeedbackPercentage"];
                    newrow["FeedbackAmount"] = dr["FeedbackAmount"] is System.DBNull ? 0 : dr["FeedbackAmount"];
                    newrow["PaymentFreight"] = dr["PaymentFreight"] is System.DBNull ? 0 : dr["PaymentFreight"];
                    newrow["PaymentFreightVAT"] = dr["PaymentFreightVAT"] is System.DBNull ? 0 : dr["PaymentFreightVAT"];
                    newrow["PaymentFreightDiscount"] = dr["PaymentFreightDiscount"] is System.DBNull ? 0 : dr["PaymentFreightDiscount"];
                    newrow["PaymentFreightDiscountVAT"] = dr["PaymentFreightDiscountVAT"] is System.DBNull ? 0 : dr["PaymentFreightDiscountVAT"];
                    newrow["RemainingFreightVAT"] = dr["RemainingFreightVAT"] is System.DBNull ? 0 : dr["RemainingFreightVAT"];
                    newrow["RemainingFreightDiscount"] = dr["RemainingFreightDiscount"] is System.DBNull ? 0 : dr["RemainingFreightDiscount"];
                    newrow["RemainingFreightDiscountVAT"] = dr["RemainingFreightDiscountVAT"] is System.DBNull ? 0 : dr["RemainingFreightDiscountVAT"];
                    newrow["DeliveryCounter"] = dr["DeliveryCounter"] is System.DBNull ? 0 : dr["DeliveryCounter"];
                    newrow["AdviceOfReceiptCode"] = dr["AdviceOfReceiptCode"] is System.DBNull ? "" : dr["AdviceOfReceiptCode"];
                    newrow["ReceiverCommuneCode"] = dr["ReceiverCommuneCode"] is System.DBNull ? "" : dr["ReceiverCommuneCode"];
                    newrow["CheckContentOnDeliveryCode"] = dr["CheckContentOnDeliveryCode"] is System.DBNull ? "" : dr["CheckContentOnDeliveryCode"];
                    newrow["DestinationPOSCode"] = dr["DestinationPOSCode"] is System.DBNull ? "" : dr["DestinationPOSCode"];
                    newrow["IsAffair"] = dr["IsAffair"] is System.DBNull ? 0 : dr["IsAffair"];
                    newrow["ReturnBeforeDate"] = dr["ReturnBeforeDate"] is System.DBNull ? dr["ReturnBeforeDate"] : dr["ReturnBeforeDate"];
                    newrow["CustomerGroupCode"] = dr["CustomerGroupCode"] is System.DBNull ? "" : dr["CustomerGroupCode"];
                    newrow["OrtherFreight"] = dr["OrtherFreight"] is System.DBNull ? 0 : dr["OrtherFreight"];
                    newrow["OriginalMainFreight"] = dr["OriginalMainFreight"] is System.DBNull ? 0 : dr["OriginalMainFreight"];
                    newrow["OriginalSubFreight"] = dr["OriginalSubFreight"] is System.DBNull ? 0 : dr["OriginalSubFreight"];
                    newrow["OriginalFuelSurchargeFreight"] = dr["OriginalFuelSurchargeFreight"] is System.DBNull ? 0 : dr["OriginalFuelSurchargeFreight"];
                    newrow["OriginalFarRegionFreight"] = dr["OriginalFarRegionFreight"] is System.DBNull ? 0 : dr["OriginalFarRegionFreight"];
                    newrow["OriginalAirSurchargeFreight"] = dr["OriginalAirSurchargeFreight"] is System.DBNull ? 0 : dr["OriginalAirSurchargeFreight"];
                    newrow["OriginalVATFreight"] = dr["OriginalVATFreight"] is System.DBNull ? 0 : dr["OriginalVATFreight"];
                    newrow["OriginalVATPercentage"] = dr["OriginalVATPercentage"] is System.DBNull ? 0 : dr["OriginalVATPercentage"];
                    newrow["OriginalTotalFreight"] = dr["OriginalTotalFreight"] is System.DBNull ? 0 : dr["OriginalTotalFreight"];
                    newrow["OriginalTotalFreightVAT"] = dr["OriginalTotalFreightVAT"] is System.DBNull ? 0 : dr["OriginalTotalFreightVAT"];
                    newrow["OriginalTotalFreightDiscount"] = dr["OriginalTotalFreightDiscount"] is System.DBNull ? 0 : dr["OriginalTotalFreightDiscount"];
                    newrow["OriginalTotalFreightDiscountVAT"] = dr["OTFDVAT"] is System.DBNull ? 0 : dr["OTFDVAT"];
                    newrow["OriginalPaymentFreight"] = dr["OriginalPaymentFreight"] is System.DBNull ? 0 : dr["OriginalPaymentFreight"];
                    newrow["OriginalPaymentFreightVAT"] = dr["OriginalPaymentFreightVAT"] is System.DBNull ? 0 : dr["OriginalPaymentFreightVAT"];
                    newrow["OriginalPaymentFreightDiscount"] = dr["OriginalPaymentFreightDiscount"] is System.DBNull ? 0 : dr["OriginalPaymentFreightDiscount"];
                    newrow["OriginalPaymentFreightDiscountVAT"] = dr["OPFDVAT"] is System.DBNull ? 0 : dr["OPFDVAT"];
                    newrow["OriginalRemainingFreight"] = dr["OriginalRemainingFreight"] is System.DBNull ? 0 : dr["OriginalRemainingFreight"];
                    newrow["OriginalRemainingFreightVAT"] = dr["OriginalRemainingFreightVAT"] is System.DBNull ? 0 : dr["OriginalRemainingFreightVAT"];
                    newrow["OriginalRemainingFreightDiscount"] = dr["ORFDiscount"] is System.DBNull ? 0 : dr["ORFDiscount"];
                    newrow["OriginalRemainingFreightDiscountVAT"] = dr["ORFDVAT"] is System.DBNull ? 0 : dr["ORFDVAT"];
                    newrow["IsEcommerce"] = dr["IsEcommerce"] is System.DBNull ? "" : dr["IsEcommerce"];
                    mMailTripDs.Tables["Item"].Rows.Add(newrow);
                    #endregion

                }
                catch (Exception e)
                {

                }
            }
            dtItem.Dispose();
        }
        private void Load_Mailtrip_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtMailTrip = new DataTable();
            E1I2 myE1I2 = new E1I2();
            //Clear table
            mMailTripDs.Tables["MailTrip"].Clear();
            //Add vao table Mailtrip
            // get list mail trip
            dtMailTrip = myE1I2.GetListMailTrip(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
            foreach (DataRow dr in dtMailTrip.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["MailTrip"].NewRow();

                    newrow["StartingCode"] = dr["StartingCode"] is System.DBNull ? "" : dr["StartingCode"];
                    newrow["DestinationCode"] = dr["DestinationCode"] is System.DBNull ? "" : dr["DestinationCode"];
                    newrow["MailtripType"] = dr["MailtripType"] is System.DBNull ? "" : dr["MailtripType"];
                    newrow["ServiceCode"] = dr["ServiceCode"] is System.DBNull ? "" : dr["ServiceCode"];
                    newrow["Year"] = dr["Year"] is System.DBNull ? "" : dr["Year"];
                    newrow["MailtripNumber"] = dr["MailtripNumber"] is System.DBNull ? "" : dr["MailtripNumber"];
                    newrow["OutgoingDate"] = dr["OutgoingDate"] is System.DBNull ? DateTime.Now : dr["OutgoingDate"];
                    newrow["Status"] = dr["Status"] is System.DBNull ? 1 : dr["Status"];
                    newrow["MailRouteCode"] = dr["MailRouteCode"] is System.DBNull ? "" : dr["MailRouteCode"];
                    newrow["BC37Number"] = dr["BC37Number"] is System.DBNull ? "" : dr["BC37Number"];
                    newrow["IncomingDate"] = dr["IncomingDate"] is System.DBNull ? dr["IncomingDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["IncomingDate"]));
                    newrow["Quantity"] = dr["Quantity"] is System.DBNull ? 0 : dr["Quantity"];
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? 0 : dr["Weight"];
                    newrow["NumberItemPerSheet"] = dr["NumberItemPerSheet"] is System.DBNull ? 35 : dr["NumberItemPerSheet"];
                    newrow["PackagingTime"] = dr["PackagingTime"] is System.DBNull ? DateTime.Now.ToString() : dr["PackagingTime"];
                    newrow["PackagingUser"] = dr["PackagingUser"] is System.DBNull ? "" : dr["PackagingUser"];
                    newrow["PackagingMachineName"] = dr["PackagingMachineName"] is System.DBNull ? "" : dr["PackagingMachineName"];
                    newrow["OpeningTime"] = dr["OpeningTime"] is System.DBNull ? dr["OpeningTime"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["OpeningTime"]));
                    newrow["OpeningUser"] = dr["OpeningUser"] is System.DBNull ? "" : dr["OpeningUser"];
                    newrow["OpeningMachineName"] = dr["OpeningMachineName"] is System.DBNull ? "" : dr["OpeningMachineName"];
                    newrow["InitialTime"] = dr["InitialTime"] is System.DBNull ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["InitialTime"]));
                    newrow["InitialMachineName"] = dr["InitialMachineName"] is System.DBNull ? "" : dr["InitialMachineName"];
                    newrow["InitialUser"] = dr["InitialUser"] is System.DBNull ? "" : dr["InitialUser"];
                    newrow["TrasferTime"] = dr["TrasferTime"] is System.DBNull ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TrasferTime"]));
                    newrow["TransferMachine"] = dr["TransferMachine"] is System.DBNull ? "" : dr["TransferMachine"];
                    newrow["TransferUser"] = dr["TransferUser"] is System.DBNull ? "" : dr["TransferUser"];
                    newrow["TransportNumber"] = dr["TransportNumber"] is System.DBNull ? "" : dr["TransportNumber"];
                    newrow["TransportCode"] = dr["TransportCode"] is System.DBNull ? "" : dr["TransportCode"];
                    newrow["OriginalTransportPOSCode"] = dr["OriginalTransportPOSCode"] is System.DBNull ? "" : dr["OriginalTransportPOSCode"];
                    newrow["TransportDate"] = dr["TransportDate"] is System.DBNull ? dr["TransportDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransportDate"]));
                    newrow["CounterCode"] = dr["CounterCode"] is System.DBNull ? "" : dr["CounterCode"];
                    newrow["DeliveryRoute"] = dr["DeliveryRoute"] is System.DBNull ? "" : dr["DeliveryRoute"];
                    newrow["Type"] = dr["Type"] is System.DBNull ? 0 : dr["Type"];
                    newrow["TransferPOSCode"] = dr["TransferPOSCode"] is System.DBNull ? "" : dr["TransferPOSCode"];
                    newrow["TransferDate"] = dr["TransferDate"] is System.DBNull ? dr["TransferDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransferDate"]));
                    newrow["TransferStatus"] = dr["TransferStatus"] is System.DBNull ? 0 : dr["TransferStatus"];
                    newrow["TransferTimes"] = dr["TransferTimes"] is System.DBNull ? 0 : dr["TransferTimes"];
                    newrow["TransferID"] = dr["TransferID"] is System.DBNull ? "" : dr["TransferID"];

                    mMailTripDs.Tables["MailTrip"].Rows.Add(newrow);
                }
                catch (Exception e)
                {
                    
                }
            }
            dtMailTrip.Dispose();
        }
        private void  Load_PostBag_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            E1I2 myE1I2 = new E1I2();
            DataTable dtPostBag = new DataTable();
            //Clear table
            mMailTripDs.Tables["PostBag"].Clear();
            //Add vao table Postbag
            dtPostBag = myE1I2.GetListPostBag(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
            foreach (DataRow dr in dtPostBag.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["PostBag"].NewRow();

                    newrow["PostBagIndex"] = dr["PostBagIndex"] is System.DBNull ? 0 : dr["PostBagIndex"];
                    newrow["PostBagTypeCode"] = dr["PostBagTypeCode"] is System.DBNull ? "" : dr["PostBagTypeCode"];
                    newrow["F"] = dr["F"] is System.DBNull ? 0 : dr["F"];
                    newrow["FromPOSCode"] = dr["FromPOSCode"] is System.DBNull ? "" : dr["FromPOSCode"];
                    newrow["ToPOSCode"] = dr["ToPOSCode"] is System.DBNull ? "" : dr["ToPOSCode"];
                    newrow["MailTripType"] = dr["MailTripType"] is System.DBNull ? "" : dr["MailTripType"];
                    newrow["ServiceCode"] = dr["ServiceCode"] is System.DBNull ? "" : dr["ServiceCode"];
                    newrow["Year"] = dr["Year"] is System.DBNull ? "" : dr["Year"];
                    newrow["MailTripNumber"] = dr["MailTripNumber"] is System.DBNull ? "" : dr["MailTripNumber"];
                    newrow["PostBagNumber"] = dr["PostBagNumber"] is System.DBNull ? "" : dr["PostBagNumber"];
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? "0" : dr["Weight"];
                    newrow["Status"] = dr["Status"] is System.DBNull ? 2 : dr["Status"];
                    newrow["Quantity"] = dr["Quantity"] is System.DBNull ? 0 : dr["Quantity"];
                    newrow["IsPrinted"] = dr["IsPrinted"] is System.DBNull ? 1 : dr["IsPrinted"];
                    newrow["BC37Date"] = dr["BC37Date"] is System.DBNull ? dr["BC37Date"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["BC37Date"]));
                    newrow["PackagingTime"] = dr["PackagingTime"] is System.DBNull ? DateTime.Now : dr["PackagingTime"];
                    newrow["PackagingUser"] = dr["PackagingUser"] is System.DBNull ? "" : dr["PackagingUser"];
                    newrow["PackagingMachine"] = dr["PackagingMachine"] is System.DBNull ? "" : dr["PackagingMachine"];
                    newrow["OpeningTime"] = dr["OpeningTime"] is System.DBNull ? dr["OpeningTime"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["OpeningTime"]));
                    newrow["OpeningMachine"] = dr["OpeningMachine"] is System.DBNull ? "" : dr["OpeningMachine"];
                    newrow["OpeningUser"] = dr["OpeningUser"] is System.DBNull ? "" : dr["OpeningUser"];
                    newrow["IncomingDate"] = dr["IncomingDate"] is System.DBNull ? dr["IncomingDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["IncomingDate"]));
                    newrow["CaseWeight"] = dr["CaseWeight"] is System.DBNull ? 0 : dr["CaseWeight"];
                    newrow["IsDiscrete"] = dr["IsDiscrete"] is System.DBNull ? 0 : dr["IsDiscrete"];
                    newrow["IsDeliveryRoute"] = dr["IsDeliveryRoute"] is System.DBNull ? 0 : dr["IsDeliveryRoute"];
                    newrow["PostBagCode"] = dr["PostBagCode"] is System.DBNull ? "" : dr["PostBagCode"];
                    newrow["Note"] = dr["Note"] is System.DBNull ? "" : dr["Note"];

                    mMailTripDs.Tables["PostBag"].Rows.Add(newrow);
                }
                catch (Exception e)
                {
                   
                }
            }
            dtPostBag.Dispose();
        }
        private void Load_Dispatch_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            E1I2 myE1I2 = new E1I2();
            DataTable dtDispatch = new DataTable();
            //Clear table
            mMailTripDs.Tables["Dispatch"].Clear();
            //Add vao table Dispatch
            dtDispatch = myE1I2.GetListDispatch(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
            foreach (DataRow dr in dtDispatch.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Dispatch"].NewRow();

                    newrow["PostBagIndex"] = dr["PostBagIndex"] is System.DBNull ? 0 : dr["PostBagIndex"];
                    newrow["FromPOSCode"] = dr["FromPOSCode"] is System.DBNull ? "" : dr["FromPOSCode"].ToString();
                    newrow["ToPOSCode"] = dr["ToPOSCode"] is System.DBNull ? "" : dr["ToPOSCode"].ToString();
                    newrow["MailTripType"] = dr["MailTripType"] is System.DBNull ? "" : dr["MailTripType"].ToString();
                    newrow["ServiceCode"] = dr["ServiceCode"] is System.DBNull ? "" : dr["ServiceCode"].ToString();
                    newrow["Year"] = dr["Year"] is System.DBNull ? "" : dr["Year"].ToString();
                    newrow["MailTripNumber"] = dr["MailTripNumber"] is System.DBNull ? "" : dr["MailTripNumber"].ToString();
                    newrow["ItemCode"] = dr["ItemCode"] is System.DBNull ? "" : dr["ItemCode"].ToString();
                    newrow["Status"] = dr["Status"] is System.DBNull ? 0 : dr["Status"];
                    newrow["IndexNumber"] = dr["IndexNumber"] is System.DBNull ? 0 : dr["IndexNumber"];
                    newrow["Sheet"] = dr["Sheet"] is System.DBNull ? 1 : dr["Sheet"];
                    newrow["DeliveryRouteCode"] = dr["DeliveryRouteCode"] is System.DBNull ? "" : dr["DeliveryRouteCode"].ToString();
                    newrow["CounterCode"] = dr["CounterCode"] is System.DBNull ? "" : dr["CounterCode"].ToString();
                    newrow["ShiftCode"] = dr["ShiftCode"] is System.DBNull ? "" : dr["ShiftCode"].ToString();

                    mMailTripDs.Tables["DisPatch"].Rows.Add(newrow);
                }
                catch (Exception e)
                {
                   
                }
            }
            dtDispatch.Dispose();
        }
       
        /// <summary>
        /// Danh sách chuyến thư đi.
        /// </summary>
        [HttpGet]
        [Route("DSChuyenThu")]
        public async Task<IHttpActionResult> GetListChThu(int bccp, DateTime ngay)
        {
            // int bccp = 0;
            //int ngay = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            // int ngay = 20160404;
            // var data = E1I2DA.GetListChThu(bccp, ngay);
            int tempngay = Convert.ToInt32(ngay.ToString("yyyyMMdd"));
            var data = E1I2DA.GetListChThu2(bccp, tempngay);
            return Ok(new ResponseCode { code = "success", message = "Danh sách thông tin phát của bưu gửi", data = data });
        }


        public static string DataTableToJSON(DataTable Dt)
        {
            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;
            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\":\"" + StrDc[i] + i.ToString() + "¾" + "\",";

            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            StringBuilder Sb = new StringBuilder();

            Sb.Append("[");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;

                for (int j = 0; j < Dt.Columns.Count; j++)
                {

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().Trim());
                }
                //Sb.AppendFormat("{{{0}}},",TempStr);

                Sb.Append("{" + TempStr + "},");
            }

            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            if (Sb.ToString().Length > 0)
                Sb.Append("]");

            return StripControlChars(Sb.ToString());

        }
        //To strip control characters:

        //A character that does not represent a printable character but //serves to initiate a particular action.

        public static string StripControlChars(string s)
        {
            return Regex.Replace(s, @"[^\x20-\x7F]", "");
        }
    }
}
