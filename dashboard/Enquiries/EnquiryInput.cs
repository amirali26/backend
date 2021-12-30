namespace dashboard.Enquiries
{
    public class EnquiryInput
    {
        public string Message { get; set; }
        public string RequestId { get; set; }
        public int InitialConsultationFee { get; set; }
        public int? EstimatedPrice { get; set; }
        public bool OfficeAppointment { get; set; }
        public bool PhoneAppointment { get; set; }
        public bool VideoCallAppointment { get; set; }
    }
}