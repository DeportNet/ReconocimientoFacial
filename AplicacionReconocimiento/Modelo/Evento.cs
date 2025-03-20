

namespace DeportNetReconocimiento.Modelo
{
    internal class Evento
    {

        private int major_type;
        private int minor_type;
        private string? major_type_description;
        private string? minor_type_description;
        private string? user;
        private string? remote_ip_address;
        private string? device_ip_address;
        private string? card_number;
        private string? card_type;
        private int card_reader_number;
        private int door_number;
        private int multiple_card_authentication_serial_number;
        private int alarm_input_number;
        private int alarm_output_number;
        private int event_trigger_number;
        private int rs485_channel_number;
        private int multi_recombinant_authentication_id;
        private string? card_reader_kind;
        private int access_channel;
        private int employee_number;
        private string? device_number;
        private int local_controller_id;
        private string? internet_access;
        private string? type;
        private string? swipe_card_type;
        private string? mac_address;
        private DateTime time;
        private bool success = false;
        private string? exception;

        public int Major_Type
        {
            get { return major_type; }
            set { major_type = value; }
        }

        public int Minor_Type
        {
            get { return minor_type; }
            set { minor_type = value; }
        }

        public string? Minor_Type_Description
        {
            get { return minor_type_description; }
            set { minor_type_description = value; }
        }

        public string? Major_Type_Description
        {
            get { return major_type_description; }
            set { major_type_description = value; }
        }

        public string? User
        {
            get { return user; }
            set { user = value; }
        }

        public string? Remote_IP_Address
        {
            get { return remote_ip_address; }
            set { remote_ip_address = value; }
        }

        public string? Device_IP_Address
        {
            get { return device_ip_address; }
            set { device_ip_address = value; }
        }

        public string? Card_Number
        {
            get { return card_number; }
            set { card_number = value; }
        }

        public string? Card_Type
        {
            get { return card_type; }
            set { card_type = value; }
        }

        public int Card_Reader_Number
        {
            get { return card_reader_number; }
            set { card_reader_number = value; }
        }

        public int Door_Number
        {
            get { return door_number; }
            set { door_number = value; }
        }

        public int Multiple_Card_Authentication_Serial_Number
        {
            get { return multiple_card_authentication_serial_number; ; }
            set { multiple_card_authentication_serial_number = value; }
        }

        public int Alarm_Input_Number
        {
            get { return alarm_input_number; ; }
            set { alarm_input_number = value; }
        }

        public int Alarm_Output_Number
        {
            get { return alarm_output_number; }
            set { alarm_output_number = value; }
        }

        public int Event_Trigger_Number
        {
            get { return event_trigger_number; }
            set { event_trigger_number = value; }
        }

        public int RS485_Channel_Number
        {
            get { return rs485_channel_number; }
            set { rs485_channel_number = value; }
        }

        public int Multi_Recombinant_Authentication_ID
        {
            get { return multi_recombinant_authentication_id; }
            set { multi_recombinant_authentication_id = value; }
        }

        public string? Card_Reader_Kind
        {
            get { return card_reader_kind; }
            set { card_reader_kind = value; }
        }

        public int Access_Channel
        {
            get { return access_channel; }
            set { access_channel = value; }
        }


        public int Employee_Number
        {
            get { return employee_number; }
            set { employee_number = value; }
        }

        public string? Device_Number
        {
            get { return device_number; }
            set { device_number = value; }
        }

        public int Local_Controller_ID
        {
            get { return local_controller_id; }
            set { local_controller_id = value; }
        }

        public string? Internet_Access
        {
            get { return internet_access; }
            set { internet_access = value; }
        }

        public string? Type
        {
            get { return type; }
            set { type = value; }
        }


        public string? Swipe_Card_Type
        {
            get { return swipe_card_type; }
            set { swipe_card_type = value; }
        }

        public string? Mac_Address
        {
            get { return mac_address; }
            set { mac_address = value; }
        }


        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        public string? Exception
        {
            get { return exception; }
            set { exception = value; }
        }
    }
}

