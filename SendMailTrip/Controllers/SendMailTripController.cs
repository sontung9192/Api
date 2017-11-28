using DataAccess;
using SendMailTrip.ExchangeService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;

namespace SendMailTrip.Controllers
{
    [RoutePrefix("api/ExchangeService")]
    public class SendMailTripController : ApiController
    {
        string WCFUser = ConfigurationManager.AppSettings["WCFUser"];
        string WCFPass = ConfigurationManager.AppSettings["WCFPass"];
        ServiceClient serviceClient = new ExchangeService.ServiceClient();
        MailTripDataSet mMailTripDs = new MailTripDataSet();
        SendMailTripBLL _dbClient = new SendMailTripBLL();

        int v_MaBcDong = 110040;// 101007;//703000
        int v_MaBcNhan = 108104;// 100916;
        int v_LoaiChuyenThu = 3; // k chẹck
        string v_LoaiDichVu = "";
        int v_Ngay = 20171014;// 20160405;
        int v_SoChuyenThu = 608;// 1;
        [HttpGet]
        [Route("AddMailTrip")]
        public async Task<IHttpActionResult> AddMailTrip()
        {
            // data test getlistMailtrip 
            //Load_Batch_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            //Load_Dispatch_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            //Load_PostBag_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            // Load_Mailtrip_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            //Load_Item_DongDi(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            var temp = _dbClient.GetListSmartphone_BCCP_BD13_Di().FirstOrDefault();
            if(temp != null)
            {
                v_MaBcDong = temp.Ma_Bc_Khai_Thac;
                v_MaBcNhan = temp.Ma_Bc;
                v_SoChuyenThu = temp.So_Chuyen_Thu;
                v_Ngay = temp.Ngay_Dong;
            }
            
            Load_Batch_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            Load_Dispatch_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            Load_PostBag_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            Load_Mailtrip_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            Load_Item_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            //string serviceReturn = string.Empty;
            //serviceReturn = serviceClient.AddMailtrip(mMailTripDs, WCFUser, WCFPass);
            var data = mMailTripDs;
            return Ok(new ResponseCode { code = "success", message = "Get AddMailTrip", data = data });
        }
        [HttpGet]
        [Route("GetMailTrip")]
        public async Task<IHttpActionResult> GetMailTrip()
        {
            try
            {
                var data = serviceClient.GetMailTrip(v_MaBcDong.ToString(), v_MaBcNhan.ToString(), "3", "E", v_Ngay.ToString(), v_SoChuyenThu.ToString(), WCFUser, WCFPass);
                return Ok(new ResponseCode { code = "success", message = "Get AddMailTrip", data = data });
            }
            catch(Exception ex)
            {
                return Ok(new ResponseCode { code = "success", message = "Get AddMailTrip , ex =" + ex.Message, data = null });
            }
           
        }


        [HttpGet]
        [Route("TestUpdate")]
        public async Task<IHttpActionResult> TestUpdate()
        {
            try
            {
                var data = _dbClient.UpdateSmartphone_BCCP_BD13_Di(6, 6);
                return Ok(new ResponseCode { code = "success", message = data.ToString() });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseCode { code = "success", message = "Get AddMailTrip , ex =" + ex.Message, data = null });
            }

        }
        [HttpGet]
        [Route("GetListSmartphone_BCCP_BD13_Di")]
        public async Task<IHttpActionResult> GetListSmartphone_BCCP_BD13_Di()
        {
            try
            {
                var data = _dbClient.GetListSmartphone_BCCP_BD13_Di();
                return Ok(new ResponseCode { code = "success", message = "Thông tin chuyến thư sẵn sàng bắn" , data = data });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseCode { code = "success", message = "Get AddMailTrip , ex =" + ex.Message, data = null });
            }

        }
        #region Load Data
        private void Load_Item_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            mMailTripDs.Tables["Item"].Clear();

            //Add vao table Item
            var dtItem = _dbClient.GetListItem_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            foreach (var item in dtItem)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Item"].NewRow();
                    #region
                    newrow["ItemCode"] = item.ItemCode;
                    newrow["AcceptancePOSCode"] = item.AcceptancePOSCode == null ? "" : item.AcceptancePOSCode;
                    newrow["SenderFullname"] = item.SenderFullname == null ? "" : item.SenderFullname;
                    newrow["SenderAddress"] = item.SenderAddress == null ? "" : item.SenderAddress;
                    newrow["CustomerCode"] = item.CustomerCode == null ? "" : item.CustomerCode;
                    newrow["BatchCode"] = item.BatchCode == null ? "" : item.BatchCode;
                    newrow["ReceiverFullname"] = item.ReceiverFullname == null ? "" : item.ReceiverFullname;
                    newrow["ReceiverTel"] = item.ReceiverTel == null ? "" : item.ReceiverTel;
                    newrow["ReceiverAddress"] = item.ReceiverAddress == null ? "" : item.ReceiverAddress;
                    newrow["isDomestic"] = item.isDomestic;
                    newrow["CountryCode"] = item.CountryCode == null ? "" : item.CountryCode;
                    newrow["POSCode"] = item.POSCode.ToString();
                    newrow["IsStatePrice"] = item.IsStatePrice;
                    newrow["StatePriceValue"] = item.StatePriceValue;
                    newrow["SendingContent"] = item.SendingContent.ToString();
                    newrow["Note"] = item.Note.ToString();
                    newrow["ItemTypeCode"] = item.ItemTypeCode == null ? "" : item.ItemTypeCode;
                    newrow["SendingTime"] = item.SendingTime == null ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(item.SendingTime));
                    newrow["IsAirmail"] = item.IsAirmail;
                    newrow["Weight"] = item.Weight;
                    newrow["Status"] = item.Status;
                    newrow["TotalFreight"] = item.TotalFreight;
                    newrow["EmployeeCode"] = item.EmployeeCode.ToString();
                    newrow["SenderJob"] = item.SenderJob == null ? "" : item.SenderJob;
                    newrow["ProvinceCode"] = item.ProvinceCode == null ? "" : item.ProvinceCode;
                    newrow["LightItem"] = item.LightItem;
                    newrow["SectionCode"] = item.SectionCode == null ? "" : item.SectionCode;
                    newrow["ReceiverJob"] = item.ReceiverJob == null ? "" : item.ReceiverJob;
                    newrow["IsOpened"] = item.IsOpened;
                    newrow["CertificateNumber"] = item.CertificateNumber == null ? "" : item.CertificateNumber;
                    newrow["LicenseNumber"] = item.LicenseNumber == null ? "" : item.LicenseNumber;
                    newrow["InvoiceNumber"] = item.InvoiceNumber == null ? "" : item.InvoiceNumber;
                    newrow["SenderIdentification"] = item.SenderIdentification == null ? "" : item.SenderIdentification;
                    newrow["SenderIssueDate"] = item.SenderIssueDate;
                    newrow["SenderIssueCountry"] = item.SenderIssueCountry == null ? "" : item.SenderIssueCountry;
                    newrow["ReceiverIdentification"] = item.ReceiverIdentification == null ? "" : item.ReceiverIdentification;
                    newrow["ReceiverIssueDate"] = item.ReceiverIssueDate;
                    newrow["ReceiverIssueCountry"] = item.ReceiverIssueCountry == null ? "" : item.ReceiverIssueCountry;
                    newrow["SenderTel"] = item.SenderTel == null ? "" : item.SenderTel;
                    newrow["MainFreight"] = item.MainFreight;
                    newrow["VATFreight"] = item.VATFreight;
                    newrow["SubFreight"] = item.SubFreight;
                    newrow["IsPostFree"] = item.IsPostFree;
                    newrow["StatePriceFreight"] = item.StatePriceFreight;
                    newrow["PrintedNumber"] = item.PrintedNumber;
                    newrow["DataCode"] = item.DataCode == null ? "" : item.DataCode;
                    newrow["SenderCustomReference"] = item.SenderCustomReference == null ? "" : item.SenderCustomReference;
                    newrow["RemainingFreight"] = item.RemainingFreight;
                    newrow["ReceiverCustomReference"] = item.ReceiverCustomReference == null ? "" : item.ReceiverCustomReference;
                    newrow["IsReturn"] = item.IsReturn;
                    newrow["IsCompensate"] = item.IsCompensate;
                    newrow["IsForward"] = item.IsForward;
                    newrow["IsAirmailForward"] = item.IsAirmailForward;
                    newrow["IsAirmailReturn"] = item.IsAirmailReturn;
                    newrow["IsDebt"] = item.IsDebt;
                    newrow["MachineName"] = item.MachineName == null ? "" : item.MachineName;
                    newrow["AcceptedIndex"] = item.AcceptedIndex;
                    newrow["BC16Index"] = item.BC16Index;
                    newrow["IncomingIndex"] = item.IncomingIndex;
                    newrow["ServiceCode"] = item.ServiceCode == null ? "E" : item.ServiceCode;
                    newrow["ReceiverDistrictCode"] = item.ReceiverDistrictCode == null ? "" : item.ReceiverDistrictCode.ToString();
                    newrow["LetterMoneyOrderFreight"] = item.LetterMoneyOrderFreight;
                    newrow["ValueAddedServiceFreightTotalFreight"] = item.VASFTF;
                    newrow["OrderCode"] = item.OrderCode == null ? "" : item.OrderCode;
                    newrow["ReceiverAddressCode"] = item.ReceiverAddressCode == null ? "" : item.ReceiverAddressCode.ToString();
                    newrow["SenderMobile"] = item.SenderMobile == null ? "" : item.SenderMobile;
                    newrow["SenderFax"] = item.SenderFax == null ? "" : item.SenderFax;
                    newrow["SenderEmail"] = item.SenderEmail == null ? "" : item.SenderEmail;
                    newrow["ReceiverMobile"] = item.ReceiverMobile == null ? "" : item.ReceiverMobile;
                    newrow["ReceiverFax"] = item.ReceiverFax == null ? "" : item.ReceiverFax;
                    newrow["ReceiverEmail"] = item.ReceiverEmail == null ? "" : item.ReceiverEmail;
                    newrow["Discount"] = item.Discount;
                    newrow["Abatement"] = item.Abatement;
                    newrow["UndeliverableGuide"] = item.UndeliverableGuide;
                    newrow["Width"] = item.Width;
                    newrow["Height"] = item.Height;
                    newrow["Length"] = item.Length;
                    newrow["CheckSum"] = item.CheckSum == null ? "" : item.CheckSum;
                    newrow["ItemNumber"] = item.ItemNumber == null ? "" : item.ItemNumber;
                    newrow["ExchangeRateCode"] = item.ExchangeRateCode == null ? "" : item.ExchangeRateCode;
                    newrow["CODAddress"] = item.CODAddress == null ? "" : item.CODAddress;
                    newrow["CODPayment"] = item.CODPayment;
                    newrow["SenderDistrictCode"] = item.SenderDistrictCode == null ? "" : item.SenderDistrictCode;
                    newrow["ReceiverContact"] = item.ReceiverContact == null ? "" : item.ReceiverContact;
                    newrow["UndeliverableReason"] = item.UndeliverableReason;
                    newrow["DecisionNo"] = item.DecisionNo;
                    newrow["DecisionDate"] = item.DecisionDate;
                    newrow["ReturnDayNumber"] = item.ReturnDayNumber;
                    newrow["DiscountPercentage"] = item.DiscountPercentage;
                    newrow["DiscountAmount"] = item.DiscountAmount;
                    newrow["ExecuteOrder"] = item.ExecuteOrder;
                    newrow["InvoiceAttached"] = item.InvoiceAttached;
                    newrow["OtherAttached"] = item.OtherAttached;
                    newrow["OtherAttachedInfor"] = item.OtherAttachedInfor;
                    newrow["TransferMachine"] = item.TransferMachine;
                    newrow["TransferUser"] = item.TransferUser;
                    newrow["TransferPOSCode"] = item.TransferPOSCode;
                    newrow["TransferDate"] = item.TransferDate == null ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(item.TransferDate));
                    newrow["TransferStatus"] = item.TransferStatus;
                    newrow["TransferTimes"] = item.TransferTimes;
                    newrow["WeightConvert"] = item.WeightConvert;
                    newrow["IsDiscount"] = item.IsDiscount;
                    newrow["InvoiceExport"] = item.InvoiceExport;
                    newrow["Total"] = item.Total;
                    newrow["AcceptedType"] = item.AcceptedType;
                    newrow["SenderTaxCode"] = item.SenderTaxCode;
                    newrow["VATPercentage"] = item.VATPercentage;
                    newrow["FuelSurchargeFreight"] = item.FuelSurchargeFreight;
                    newrow["FarRegionFreight"] = item.FarRegionFreight;
                    newrow["AirSurchargeFreight"] = item.AirSurchargeFreight;
                    newrow["OtherFreight"] = item.OtherFreight;
                    newrow["TotalFreightVAT"] = item.TotalFreightVAT;
                    newrow["TotalFreightDiscount"] = item.TotalFreightDiscount;
                    newrow["TotalFreightDiscountVAT"] = item.TotalFreightDiscountVAT;
                    newrow["ReceiverTaxCode"] = item.ReceiverTaxCode == null ? "" : item.ReceiverTaxCode;
                    newrow["FarRegion"] = item.FarRegion;
                    newrow["IsCollection"] = item.IsCollection;
                    newrow["CustomerAccountNo"] = item.CustomerAccountNo == null ? "" : item.CustomerAccountNo;
                    newrow["IsFeedback"] = item.IsFeedback;
                    newrow["FeedbackPercentage"] = item.FeedbackPercentage;
                    newrow["FeedbackAmount"] = item.FeedbackAmount;
                    newrow["PaymentFreight"] = item.PaymentFreight;
                    newrow["PaymentFreightVAT"] = item.PaymentFreightVAT;
                    newrow["PaymentFreightDiscount"] = item.PaymentFreightDiscount;
                    newrow["PaymentFreightDiscountVAT"] = item.PaymentFreightDiscountVAT;
                    newrow["RemainingFreightVAT"] = item.RemainingFreightVAT;
                    newrow["RemainingFreightDiscount"] = item.RemainingFreightDiscount;
                    newrow["RemainingFreightDiscountVAT"] = item.RemainingFreightDiscountVAT;
                    newrow["DeliveryCounter"] = item.DeliveryCounter;
                    newrow["AdviceOfReceiptCode"] = item.AdviceOfReceiptCode == null ? "" : item.AdviceOfReceiptCode;
                    newrow["ReceiverCommuneCode"] = item.ReceiverCommuneCode == null ? "" : item.ReceiverCommuneCode;
                    newrow["CheckContentOnDeliveryCode"] = item.CheckContentOnDeliveryCode == null ? "" : item.CheckContentOnDeliveryCode;
                    newrow["DestinationPOSCode"] = item.DestinationPOSCode == null ? "" : item.DestinationPOSCode;
                    newrow["IsAffair"] = item.IsAffair;
                    newrow["ReturnBeforeDate"] = item.ReturnBeforeDate == null ? item.ReturnBeforeDate : item.ReturnBeforeDate;
                    newrow["CustomerGroupCode"] = item.CustomerGroupCode == null ? "" : item.CustomerGroupCode;
                    newrow["OrtherFreight"] = item.OrtherFreight;
                    newrow["OriginalMainFreight"] = item.OriginalMainFreight;
                    newrow["OriginalSubFreight"] = item.OriginalSubFreight;
                    newrow["OriginalFuelSurchargeFreight"] = item.OriginalFuelSurchargeFreight;
                    newrow["OriginalFarRegionFreight"] = item.OriginalFarRegionFreight;
                    newrow["OriginalAirSurchargeFreight"] = item.OriginalAirSurchargeFreight;
                    newrow["OriginalVATFreight"] = item.OriginalVATFreight;
                    newrow["OriginalVATPercentage"] = item.OriginalVATPercentage;
                    newrow["OriginalTotalFreight"] = item.OriginalTotalFreight;
                    newrow["OriginalTotalFreightVAT"] = item.OriginalTotalFreightVAT;
                    newrow["OriginalTotalFreightDiscount"] = item.OriginalTotalFreightDiscount;
                    newrow["OriginalTotalFreightDiscountVAT"] = item.OTFDVAT;
                    newrow["OriginalPaymentFreight"] = item.OriginalPaymentFreight;
                    newrow["OriginalPaymentFreightVAT"] = item.OriginalPaymentFreightVAT;
                    newrow["OriginalPaymentFreightDiscount"] = item.OriginalPaymentFreightDiscount;
                    newrow["OriginalPaymentFreightDiscountVAT"] = item.OPFDVAT;
                    newrow["OriginalRemainingFreight"] = item.OriginalRemainingFreight;
                    newrow["OriginalRemainingFreightVAT"] = item.OriginalRemainingFreightVAT;
                    newrow["OriginalRemainingFreightDiscount"] = item.ORFDiscount;
                    newrow["OriginalRemainingFreightDiscountVAT"] = item.ORFDVAT;
                    newrow["IsEcommerce"] = item.IsEcommerce;
                    mMailTripDs.Tables["Item"].Rows.Add(newrow);
                    #endregion

                }
                catch (Exception e)
                {

                }
            }
        }
        private void Load_Mailtrip_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            //Clear table
            mMailTripDs.Tables["MailTrip"].Clear();
            //Add vao table Mailtrip
            // get list mail trip
            var dtMailTrip = _dbClient.GetListMailTrip_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);

            foreach (var item in dtMailTrip)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["MailTrip"].NewRow();
                    newrow["StartingCode"] = item.StartingCode;
                    newrow["DestinationCode"] = item.DestinationCode;
                    newrow["MailtripType"] = item.MailtripType == null ? "" : item.MailtripType;
                    newrow["ServiceCode"] = item.ServiceCode == null ? "" : item.ServiceCode;
                    newrow["Year"] = item.Year;
                    newrow["MailtripNumber"] = item.MailtripNumber;
                    newrow["OutgoingDate"] = item.OutgoingDate == null ? DateTime.Now : item.OutgoingDate;
                    newrow["Status"] = item.Status;
                    newrow["MailRouteCode"] = item.MailRouteCode == null ? "" : item.MailRouteCode;
                    newrow["BC37Number"] = item.BC37Number == null ? "" : item.BC37Number;
                    newrow["IncomingDate"] = item.IncomingDate;
                    newrow["Quantity"] = item.Quantity;
                    newrow["Weight"] = item.Weight;
                    newrow["NumberItemPerSheet"] = item.NumberItemPerSheet;
                    newrow["PackagingTime"] = item.PackagingTime == null ? DateTime.Now : item.PackagingTime;
                    newrow["PackagingUser"] = item.PackagingUser;
                    newrow["PackagingMachineName"] = item.PackagingMachineName == null ? "" : item.PackagingMachineName;
                    newrow["OpeningTime"] = item.OpeningTime;
                    newrow["OpeningUser"] = item.OpeningUser == null ? "" : item.OpeningUser;
                    newrow["OpeningMachineName"] = item.OpeningMachineName == null ? "" : item.OpeningMachineName;
                    newrow["InitialTime"] = item.InitialTime == null ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(item.InitialTime));
                    newrow["InitialMachineName"] = item.InitialMachineName == null ? "" : item.InitialMachineName;
                    newrow["InitialUser"] = item.InitialUser;
                    newrow["TrasferTime"] = item.TrasferTime == null ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(item.TrasferTime));
                    newrow["TransferMachine"] = item.TransferMachine == null ? "" : item.TransferMachine;
                    newrow["TransferUser"] = item.TransferUser == null ? "" : item.TransferUser;
                    newrow["TransportNumber"] = item.TransportNumber == null ? "" : item.TransportNumber;
                    newrow["TransportCode"] = item.TransportCode == null ? "" : item.TransportCode;
                    newrow["OriginalTransportPOSCode"] = item.OriginalTransportPOSCode == null ? "" : item.OriginalTransportPOSCode;
                    newrow["TransportDate"] = item.TransportDate;
                    newrow["CounterCode"] = item.CounterCode == null ? "" : item.CounterCode;
                    newrow["DeliveryRoute"] = item.DeliveryRoute;
                    newrow["Type"] = item.Type;
                    newrow["TransferPOSCode"] = item.TransferPOSCode == null ? "" : item.TransferPOSCode;
                    newrow["TransferDate"] = item.TransferDate;
                    newrow["TransferStatus"] = item.TransferStatus;
                    newrow["TransferTimes"] = item.TransferTimes;
                    newrow["TransferID"] = item.TransferID == null ? "" : item.TransferID;

                    mMailTripDs.Tables["MailTrip"].Rows.Add(newrow);
                }
                catch (Exception e)
                {

                }
            }
        }
        private void Load_PostBag_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            //Clear table
            mMailTripDs.Tables["PostBag"].Clear();
            //Add vao table Postbag
            var dtPostBag = _dbClient.GetListPostBag_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            foreach (var item in dtPostBag)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["PostBag"].NewRow();

                    newrow["PostBagIndex"] = item.PostBagIndex;
                    newrow["PostBagTypeCode"] = item.PostBagTypeCode == null ? "" : item.PostBagTypeCode;
                    newrow["F"] = item.F;
                    newrow["FromPOSCode"] = item.FromPOSCode;
                    newrow["ToPOSCode"] = item.ToPOSCode;
                    newrow["MailTripType"] = item.MailTripType == null ? "" : item.MailTripType;
                    newrow["ServiceCode"] = item.ServiceCode == null ? "" : item.ServiceCode;
                    newrow["Year"] = item.Year;
                    newrow["MailTripNumber"] = item.MailTripNumber;
                    newrow["PostBagNumber"] = item.PostBagNumber;
                    newrow["Weight"] = item.Weight;
                    newrow["Status"] = item.Status;
                    newrow["Quantity"] = item.Quantity;
                    newrow["IsPrinted"] = item.IsPrinted;
                    newrow["BC37Date"] = item.BC37Date;
                    newrow["PackagingTime"] = item.PackagingTime;
                    newrow["PackagingUser"] = item.PackagingUser;
                    newrow["PackagingMachine"] = item.PackagingMachine == null ? "" : item.PackagingMachine;
                    newrow["OpeningTime"] = item.OpeningTime;
                    newrow["OpeningMachine"] = item.OpeningMachine == null ? "" : item.OpeningMachine;
                    newrow["OpeningUser"] = item.OpeningUser == null ? "" : item.OpeningUser;
                    newrow["IncomingDate"] = item.IncomingDate;
                    newrow["CaseWeight"] = item.CaseWeight;
                    newrow["IsDiscrete"] = item.IsDiscrete;
                    newrow["IsDeliveryRoute"] = item.IsDeliveryRoute;
                    newrow["PostBagCode"] = item.PostBagCode == null ? "" : item.PostBagCode;
                    newrow["Note"] = item.Note == null ? "" : item.Note;

                    mMailTripDs.Tables["PostBag"].Rows.Add(newrow);
                }
                catch (Exception e)
                {

                }
            }
        }
        private void Load_Dispatch_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            //Clear table
            mMailTripDs.Tables["Dispatch"].Clear();
            //Add vao table Dispatch
            var dtDispatch = _dbClient.GetListDispatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            foreach (var item in dtDispatch)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Dispatch"].NewRow();

                    newrow["PostBagIndex"] = item.PostBagIndex;
                    newrow["FromPOSCode"] = item.FromPOSCode;
                    newrow["ToPOSCode"] = item.ToPOSCode;
                    newrow["MailTripType"] = item.MailTripType == null ? "" : item.MailTripType.ToString();
                    newrow["ServiceCode"] = item.ServiceCode == null ? "" : item.ServiceCode.ToString();
                    newrow["Year"] = item.Year;
                    newrow["MailTripNumber"] = item.MailTripNumber;
                    newrow["ItemCode"] = item.ItemCode == null ? "" : item.ItemCode.ToString();
                    newrow["Status"] = item.Status;
                    newrow["IndexNumber"] = item.IndexNumber;
                    newrow["Sheet"] = item.Sheet;
                    newrow["DeliveryRouteCode"] = item.DeliveryRouteCode;
                    newrow["CounterCode"] = item.CounterCode;
                    newrow["ShiftCode"] = item.ShiftCode;

                    mMailTripDs.Tables["DisPatch"].Rows.Add(newrow);
                }
                catch (Exception e)
                {

                }
            }
        }
        private void Load_Batch_DongDi(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            mMailTripDs.Tables["Batch"].Clear();
            //Add vao table Batch
            var dtBatch = _dbClient.GetListBatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            DataRow newrow = mMailTripDs.Tables["Batch"].NewRow();
            foreach (var item in dtBatch)
            {
                try
                {
                    newrow["BatchCode"] = item.BatchCode == null ? "" : item.BatchCode.ToString();
                    newrow["POSCode"] = item.POSCode;
                    newrow["CustomerCode"] = item.CustomerCode == null ? "" : item.CustomerCode.ToString();
                    newrow["MainFreight"] = item.MainFreight;
                    newrow["Discount"] = item.Discount;
                    newrow["Abatement"] = item.Abatement;
                    newrow["TotalFreight"] = item.TotalFreight;
                    newrow["OrderCode"] = item.OrderCode == null ? "" : item.OrderCode.ToString();
                    newrow["InvoiceAttached"] = item.InvoiceAttached;
                    newrow["OtherAttached"] = item.OtherAttached;
                    newrow["OtherAttachedInfor"] = item.OtherAttachedInfor == null ? "" : item.OtherAttachedInfor.ToString();

                    mMailTripDs.Tables["Batch"].Rows.Add(newrow);
                }
                catch (Exception ex)
                {
                    //EventLog.WriteEntry("Host2Host", "Lỗi Batch đi", EventLogEntryType.Error);
                }
            }
        }


        private void Load_Batch_DongDi1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtBatch = new DataTable();
            //Add vao table Batch
            dtBatch = _dbClient.DataSet_GetListBatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];

            foreach (DataRow dr in dtBatch.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Batch"].NewRow();
                    newrow["BatchCode"] = dr["BatchCode"] is System.DBNull ? "" : dr["BatchCode"].ToString();
                    newrow["POSCode"] = dr["POSCode"] is System.DBNull ? "" : dr["POSCode"].ToString();
                    newrow["CustomerCode"] = dr["CustomerCode"] is System.DBNull ? "" : dr["CustomerCode"].ToString();
                    newrow["MainFreight"] = dr["MainFreight"] is System.DBNull ? 0 : dr["MainFreight"];
                    newrow["Discount"] = dr["Discount"] is System.DBNull ? 0 : dr["Discount"];
                    newrow["Abatement"] = dr["Abatement"] is System.DBNull ? 0 : dr["Abatement"];
                    newrow["TotalFreight"] = dr["TotalFreight"] is System.DBNull ? 0 : dr["TotalFreight"];
                    newrow["OrderCode"] = dr["OrderCode"] is System.DBNull ? "" : dr["OrderCode"].ToString();
                    newrow["InvoiceAttached"] = dr["InvoiceAttached"] is System.DBNull ? 0 : dr["InvoiceAttached"];
                    newrow["OtherAttached"] = dr["OtherAttached"] is System.DBNull ? 0 : dr["OtherAttached"];
                    newrow["OtherAttachedInfor"] = dr["OtherAttachedInfor"] is System.DBNull ? "" : dr["OtherAttachedInfor"].ToString();

                    mMailTripDs.Tables["Batch"].Rows.Add(newrow);
                }
                catch (Exception ex)
                {
                    LogHelper.LogInfo(string.Format("[DAL][Load_Batch_DongDi1],ex={0}", ex.Message));
                    //EventLog.WriteEntry("Host2Host", "Lỗi Batch đi", EventLogEntryType.Error);
                }
            }
            dtBatch.Dispose();
        }
        private void Load_Item_DongDi1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtItem = new DataTable();
            mMailTripDs.Tables["Item"].Clear();

            //Add vao table Item
            dtItem = _dbClient.DataSet_GetListItem_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
            foreach (DataRow dr in dtItem.Rows)
            {
                try
                {
                    DataRow newrow = mMailTripDs.Tables["Item"].NewRow();
                    #region item
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
                    #region trace item
                    //Load TraceItem dong di

                    DataRow newrowTraceItem = mMailTripDs.Tables["TraceItem"].NewRow();

                    newrowTraceItem["TraceIndex"] = dr["TraceIndex1"] is System.DBNull ? 0 : dr["TraceIndex1"];
                    newrowTraceItem["ItemCode"] = dr["ItemCode1"] is System.DBNull ? "" : dr["ItemCode1"].ToString();
                    newrowTraceItem["POSCode"] = dr["POSCode1"] is System.DBNull ? "" : dr["POSCode1"].ToString();
                    newrowTraceItem["Status"] = dr["Status1"] is System.DBNull ? 0 : dr["Status1"];
                    newrowTraceItem["TraceDate"] = dr["TraceDate1"] is System.DBNull ? DateTime.Now : dr["TraceDate1"];
                    newrowTraceItem["StatusDesc"] = dr["StatusDesc1"] is System.DBNull ? "" : dr["StatusDesc1"].ToString();
                    newrowTraceItem["Note"] = dr["Note1"] is System.DBNull ? "" : dr["Note1"].ToString();
                    newrowTraceItem["TransferMachine"] = dr["TransferMachine1"] is System.DBNull ? "" : dr["TransferMachine1"].ToString();
                    newrowTraceItem["TransferUser"] = dr["TransferUser1"] is System.DBNull ? "" : dr["TransferUser1"].ToString();
                    newrowTraceItem["TransferPOSCode"] = dr["TransferPOSCode1"] is System.DBNull ? "" : dr["TransferPOSCode1"].ToString();
                    newrowTraceItem["TransferDate"] = dr["TransferDate1"] is System.DBNull ? dr["TransferDate1"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransferDate1"]));
                    newrowTraceItem["TransferStatus"] = dr["TransferStatus1"] is System.DBNull ? 1 : dr["TransferStatus1"];
                    newrowTraceItem["TransferTimes"] = dr["TransferTimes1"] is System.DBNull ? 1 : dr["TransferTimes1"];

                    mMailTripDs.Tables["TraceItem"].Rows.Add(newrowTraceItem);

                    //Load TraceItem dong di
                    #endregion

                    #region value add item service
                    //Load ValueAddItemService Dong Di

                    //if (dr["ValueAddedServiceCode2"].ToString().Length > 0)
                    //{
                    //    try
                    //    {
                    //        //Ghi từng dịch vụ
                    //        int i = 0;
                    //        string To_Hop_Dich_Vu = dr["ValueAddedServiceCode2"] is System.DBNull ? "" : dr["ValueAddedServiceCode2"].ToString();
                    //        string[] Ma_Dich_Vu = To_Hop_Dich_Vu.Split(',');
                    //        foreach (string dv in Ma_Dich_Vu)
                    //        {
                    //            if (EmsCommon.ExtendServiceEms2Bccp(dv.Trim()) != "")
                    //            {
                    //                i = i + 1;
                    //                DataRow newrowServiceItem = mMailTripDs.Tables["ValueAddedServiceItem"].NewRow();
                    //                newrowServiceItem["ValueAddedServiceCode"] = EmsCommon.ExtendServiceEms2Bccp(dv.Trim());
                    //                newrowServiceItem["ServiceCode"] = dr["ServiceCode2"] is System.DBNull ? "E" : dr["ServiceCode2"].ToString();
                    //                newrowServiceItem["ItemCode"] = dr["ItemCode2"] is System.DBNull ? "" : dr["ItemCode2"].ToString();
                    //                if (i == 1)
                    //                {
                    //                    newrowServiceItem["Freight"] = dr["Freight2"] is System.DBNull ? 0 : dr["Freight2"];
                    //                    newrowServiceItem["FreightVAT"] = dr["FreightVAT2"] is System.DBNull ? 0 : dr["FreightVAT2"];
                    //                    newrowServiceItem["OriginalFreight"] = dr["OriginalFreight2"] is System.DBNull ? 0 : dr["OriginalFreight2"];
                    //                    newrowServiceItem["OriginalFreightVAT"] = dr["OriginalFreightVAT2"] is System.DBNull ? 0 : dr["OriginalFreightVAT2"];
                    //                }
                    //                else
                    //                {
                    //                    newrowServiceItem["Freight"] = 0;
                    //                    newrowServiceItem["FreightVAT"] = 0;
                    //                    newrowServiceItem["OriginalFreight"] = 0;
                    //                    newrowServiceItem["OriginalFreightVAT"] = 0;
                    //                }
                    //                newrowServiceItem["PhaseCode"] = dr["PhaseCode2"] is System.DBNull ? "" : dr["PhaseCode2"].ToString();
                    //                newrowServiceItem["AddedDate"] = dr["AddedDate2"] is System.DBNull ? DateTime.Now : dr["AddedDate2"];
                    //                newrowServiceItem["POSCode"] = dr["POSCode2"] is System.DBNull ? "" : dr["POSCode2"].ToString();
                    //                mMailTripDs.Tables["ValueAddedServiceItem"].Rows.Add(newrowServiceItem);
                    //            }
                    //        }
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        //EventLog.WriteEntry("Host2Host", "Lỗi ValueAddedServiceItem đi" + e.ToString(), EventLogEntryType.Error);
                    //        //MessageBox.Show("Loi ValueAddedServiceItem " + e.ToString());
                    //    }
                    //}

                    //Load ValueAddItemService Dong Di
                    #endregion
                }
                catch (Exception ex)
                {
                    LogHelper.LogInfo(string.Format("[DAL][Load_Item_DongDi1],ex={0}", ex.Message));
                }
            }
            dtItem.Dispose();
        }
        private void Load_Mailtrip_DongDi1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtMailTrip = new DataTable();
            //Clear table
            mMailTripDs.Tables["MailTrip"].Clear();
            //Add vao table Mailtrip
            // get list mail trip
            dtMailTrip = _dbClient.DataSet_GetListMailTrip_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
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
                    // newrow["Weight"] = dr["Weight"] is System.DBNull ? 0 : dr["Weight"];
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? "0" : ((float)Convert.ToInt32(dr["Weight"]) / 1000).ToString();
                    newrow["NumberItemPerSheet"] = dr["NumberItemPerSheet"] is System.DBNull ? 35 : dr["NumberItemPerSheet"];
                    newrow["PackagingTime"] = dr["PackagingTime"] is System.DBNull ? DateTime.Now.ToString() : dr["PackagingTime"];
                    newrow["PackagingUser"] = dr["PackagingUser"] is System.DBNull ? "" : dr["PackagingUser"];
                    newrow["PackagingMachineName"] = dr["PackagingMachineName"] is System.DBNull ? "" : dr["PackagingMachineName"];
                    newrow["OpeningTime"] = dr["OpeningTime"] is System.DBNull ? dr["OpeningTime"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["OpeningTime"]));
                    newrow["OpeningUser"] = dr["OpeningUser"] is System.DBNull ? "" : dr["OpeningUser"];
                    newrow["OpeningMachineName"] = dr["OpeningMachineName"] is System.DBNull ? "" : dr["OpeningMachineName"];
                    newrow["InitialTime"] = dr["InitialTime"] is System.DBNull ? DateTime.Now : dr["InitialTime"]; // error
                    newrow["InitialMachineName"] = dr["InitialMachineName"] is System.DBNull ? "" : dr["InitialMachineName"];
                    newrow["InitialUser"] = dr["InitialUser"] is System.DBNull ? "" : dr["InitialUser"];
                    newrow["TrasferTime"] = dr["TrasferTime"] is System.DBNull ? DateTime.Now : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TrasferTime"]));
                    newrow["TransferMachine"] = dr["TransferMachine"] is System.DBNull ? "" : dr["TransferMachine"];
                    newrow["TransferUser"] = dr["TransferUser"] is System.DBNull ? "" : dr["TransferUser"];
                    newrow["TransportNumber"] = dr["TransportNumber"] is System.DBNull ? "" : dr["TransportNumber"];
                    newrow["TransportCode"] = dr["TransportCode"] is System.DBNull ? "" : dr["TransportCode"];
                    newrow["OriginalTransportPOSCode"] = dr["OriginalTransportPOSCode"]; //is System.DBNull ? "" : dr["OriginalTransportPOSCode"];
                    newrow["TransportDate"] = dr["TransportDate"];// is System.DBNull ? dr["TransportDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransportDate"]));
                    newrow["CounterCode"] = dr["CounterCode"] is System.DBNull ? "" : dr["CounterCode"];
                    newrow["DeliveryRoute"] = dr["DeliveryRoute"];
                    newrow["Type"] = dr["Type"] is System.DBNull ? 0 : dr["Type"];
                    newrow["TransferPOSCode"] = dr["TransferPOSCode"];// is System.DBNull ? "" : dr["TransferPOSCode"];
                    newrow["TransferDate"] = dr["TransferDate"];// is System.DBNull ? dr["TransferDate"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["TransferDate"]));
                    newrow["TransferStatus"] = dr["TransferStatus"] is System.DBNull ? 0 : dr["TransferStatus"];
                    newrow["TransferTimes"] = dr["TransferTimes"] is System.DBNull ? 0 : dr["TransferTimes"];
                    newrow["TransferID"] = dr["TransferID"] is System.DBNull ? "" : dr["TransferID"];

                    mMailTripDs.Tables["MailTrip"].Rows.Add(newrow);
                }
                catch (Exception ex)
                {
                    LogHelper.LogInfo(string.Format("[DAL][Load_Mailtrip_DongDi1],ex={0}", ex.Message));
                }
            }
            dtMailTrip.Dispose();
        }
        private void Load_PostBag_DongDi1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtPostBag = new DataTable();
            //Clear table
            mMailTripDs.Tables["PostBag"].Clear();
            //Add vao table Postbag
            dtPostBag = _dbClient.DataSet_GetListPostBag_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
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
                    //newrow["Weight"] = dr["Weight"] is System.DBNull ? "0" : dr["Weight"];
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? "0" : ((float) Convert.ToInt32(dr["Weight"]) / 1000).ToString();
                    newrow["Status"] = dr["Status"] is System.DBNull ? 2 : dr["Status"];
                    newrow["Quantity"] = dr["Quantity"] is System.DBNull ? 0 : dr["Quantity"];
                    newrow["IsPrinted"] = dr["IsPrinted"] is System.DBNull ? 1 : dr["IsPrinted"];
                    newrow["BC37Date"] = dr["BC37Date"] is System.DBNull ? dr["BC37Date"] : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["BC37Date"]));
                    newrow["PackagingTime"] = dr["PackagingTime"] is System.DBNull ? DateTime.Now : dr["PackagingTime"];
                    newrow["PackagingUser"] = dr["PackagingUser"] is System.DBNull ? "" : dr["PackagingUser"];
                    newrow["PackagingMachine"] = dr["PackagingMachine"] is System.DBNull ? "" : dr["PackagingMachine"];
                    newrow["OpeningTime"] = dr["OpeningTime"];// is System.DBNull ? null : dr["OpeningTime"];// : EmsCommon.ConvertIntToDate(Convert.ToInt32(dr["OpeningTime"]));
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
                catch (Exception ex)
                {
                    LogHelper.LogInfo(string.Format("[DAL][Load_PostBag_DongDi1],ex={0}", ex.Message));
                }
            }
            dtPostBag.Dispose();
        }
        private void Load_Dispatch_DongDi1(int v_MaBcDong, int v_MaBcNhan, int v_LoaiChuyenThu, string v_LoaiDichVu, int v_Ngay, int v_SoChuyenThu)
        {
            DataTable dtDispatch = new DataTable();
            //Clear table
            mMailTripDs.Tables["Dispatch"].Clear();
            //Add vao table Dispatch
            dtDispatch = _dbClient.DataSet_GetListDispatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu).Tables[0];
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
                catch (Exception ex)
                {
                    LogHelper.LogInfo(string.Format("[DAL][Load_Dispatch_DongDi1],ex={0}", ex.Message));
                }
            }
            dtDispatch.Dispose();
        }
        #endregion
        #region GetList
        [HttpGet]
        [Route("GetListPostBagDi")]
        public async Task<IHttpActionResult> GetListPostBag_Di()
        {
            // data test getlistMailtrip 
            var data = _dbClient.GetListPostBag_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            return Ok(new ResponseCode { code = "success", message = "Get GetListPostBag_Di", data = data });
        }
        [HttpGet]
        [Route("GetListMailTripDi")]
        public async Task<IHttpActionResult> GetListMailTrip_Di()
        {
            // data test getlistMailtrip 
            var data = _dbClient.GetListMailTrip_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            return Ok(new ResponseCode { code = "success", message = "Get GetListMailTrip_Di", data = data });
        }
        [HttpGet]
        [Route("GetListItemDi")]
        public async Task<IHttpActionResult> GetListItem_Di()
        {
            // data test getlistMailtrip 
            var data = _dbClient.GetListItem_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            return Ok(new ResponseCode { code = "success", message = "Get GetListItem_Di", data = data });
        }
        [HttpGet]
        [Route("GetListDispatchDi")]
        public async Task<IHttpActionResult> GetListDispatch_Di()
        {
            // data test getlistMailtrip 
            var data = _dbClient.GetListDispatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            return Ok(new ResponseCode { code = "success", message = "Get GetListDispatch_Di", data = data });
        }
        [HttpGet]
        [Route("GetListBatchDi")]
        public async Task<IHttpActionResult> GetListBatch_Di()
        {
            // data test getlistMailtrip 
            var data = _dbClient.GetListBatch_Di(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
            return Ok(new ResponseCode { code = "success", message = "Get GetListBatch_Di", data = data });
        }
        #endregion
    }
}
