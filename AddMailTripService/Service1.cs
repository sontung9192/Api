using AddMailTripService.ExchangeService;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Utils;

namespace AddMailTripService
{
    public partial class Service1 : ServiceBase
    {
        Timer timeDelay;
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

        public Service1()
        {
            InitializeComponent();
            timeDelay = new Timer();
            timeDelay.Interval = 5000; // run 5s each times
            timeDelay.Elapsed += new ElapsedEventHandler(StartAddMailTrip);
        }
        private void StartAddMailTrip(object sender, ElapsedEventArgs e)
        {
            try
            {
                var temp = _dbClient.GetListSmartphone_BCCP_BD13_Di().FirstOrDefault();
                if (temp != null)
                {
                    v_MaBcDong = temp.Ma_Bc_Khai_Thac;
                    v_MaBcNhan = temp.Ma_Bc;
                    v_SoChuyenThu = temp.So_Chuyen_Thu;
                    v_Ngay = temp.Ngay_Dong;

                    Load_Batch_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
                    Load_Dispatch_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
                    Load_PostBag_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
                    Load_Mailtrip_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
                    Load_Item_DongDi1(v_MaBcDong, v_MaBcNhan, v_LoaiChuyenThu, v_LoaiDichVu, v_Ngay, v_SoChuyenThu);
                    string serviceReturn = string.Empty;
                    serviceReturn = serviceClient.AddMailtrip(mMailTripDs, WCFUser, WCFPass);
                    if (serviceReturn == "00")
                    {
                        _dbClient.UpdateSmartphone_BCCP_BD13_Di(temp.Id, 1);
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.LogInfo(string.Format("[DAL][StartAddMailTrip],ex={0}", ex.Message));
            }
           
        }
        protected override void OnStart(string[] args)
        {
            timeDelay.Enabled = true;
        }

        protected override void OnStop()
        {
            timeDelay.Enabled = false;
        }
        #region Load dữ liệu
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
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? 0 : dr["Weight"];
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
                    newrow["DeliveryRoute"] = dr["DeliveryRoute"] is System.DBNull ? "" : dr["DeliveryRoute"];
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
                    newrow["Weight"] = dr["Weight"] is System.DBNull ? "0" : dr["Weight"];
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
    }
}
