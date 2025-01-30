
using System.Runtime.InteropServices;


namespace DeportNetReconocimiento.SDK
{


    public class Hik_SDK
    {

        #region HCNetSDK.dll macro definition

        #region definicion constantes facial
        public const int SERIALNO_LEN = 48; //serial number length
        public const int NET_DVR_DEV_ADDRESS_MAX_LEN = 129; //device address max length
        public const int NET_DVR_LOGIN_USERNAME_MAX_LEN = 64;   //login username max length
        public const int NET_DVR_LOGIN_PASSWD_MAX_LEN = 64; //login password max length


        public const int NET_DVR_PASSWORD_ERROR = 1;//Username or Password error
        public const int NET_DVR_USER_LOCKED = 153;

        public const int ACS_CARD_NO_LEN = 32;
        public const int MAX_CARD_READER_NUM_512 = 512;
        public const int NET_DVR_GET_FACE = 2566;//获取人脸
        public const int NET_DVR_SET_FACE = 2567;
        public const int ERROR_MSG_LEN = 32;
        public const int NET_DVR_DEL_FACE_PARAM_CFG = 2509;
        public const int NET_DVR_CAPTURE_FACE_INFO = 2510;//采集人脸
        public const int LOG_INFO_LEN = 11840; // log append information 



        public const int NET_SDK_GET_NEXT_STATUS_SUCCESS = 1000;
        public const int NET_SDK_GET_NEXT_STATUS_NEED_WAIT = 1001;
        public const int NET_SDK_GET_NEXT_STATUS_FINISH = 1002;
        public const int NET_SDK_GET_NEXT_STATUS_FAILED = 1003;
        public const int NET_SDK_GET_NEXT_STATUS_TIMEOUT = 1004;

        /*Codigos de eventos*/
        public const int NET_SDK_MONITOR_ID_LEN = 64;
        public const int NET_SDK_EMPLOYEE_NO_LEN = 32;
        public const int NET_DVR_GET_ACS_EVENT = 2514;//Obtneción de eventos del dispositivo 

        public const int MAX_NAMELEN = 16;
        public const int MACADDR_LEN = 6;

        public const int MAX_FACE_NUM = 2;

        public const int ACS_ABILITY = 0x801; //acs ability
            #endregion

        #region definicion constantes tarjeta

        public const int NAME_LEN = 32;// name length
        public const int MAX_DOOR_NUM_256 = 256; //max door num
        public const int MAX_GROUP_NUM_128 = 128; //The largest number of grou
        public const int CARD_PASSWORD_LEN = 8;   // card password len 

        public const int NET_DVR_GET_CARD = 2560;
        public const int NET_DVR_SET_CARD = 2561;
        public const int NET_DVR_DEL_CARD = 2562;


        public enum NET_SDK_SENDWITHRECV_STATUS
        {
            NET_SDK_CONFIG_STATUS_SUCCESS = 1000,    // 成功读取到数据，客户端处理完本次数据后需要再次调用NET_DVR_SendWithRecvRemoteConfig获取下一条数据
            NET_SDK_CONFIG_STATUS_NEEDWAIT = 1001,          // 配置等待，客户端可重新NET_DVR_SendWithRecvRemoteConfig
            NET_SDK_CONFIG_STATUS_FINISH = 1002,            // 数据全部取完，此时客户端可调用NET_DVR_StopRemoteConfig结束
            NET_SDK_CONFIG_STATUS_FAILED = 1003,            // 配置失败，客户端可重新NET_DVR_SendWithRecvRemoteConfig
            NET_SDK_CONFIG_STATUS_EXCEPTION = 1004,         // 配置异常，此时客户端可调用NET_DVR_StopRemoteConfig结束
        }

        #endregion

        #region definicion constantes eventos


        public const int COMM_ALARM_ACS = 0x5002; //access card alarm

        /* Alarm */
        // Main Type
        public const int MAJOR_ALARM = 0x1;
        // Hypo- Type
        public const int MINOR_ALARMIN_SHORT_CIRCUIT = 0x400; // region short circuit 
        public const int MINOR_ALARMIN_BROKEN_CIRCUIT = 0x401; // region broken circuit
        public const int MINOR_ALARMIN_EXCEPTION = 0x402; // region exception 
        public const int MINOR_ALARMIN_RESUME = 0x403; // region resume 
        public const int MINOR_HOST_DESMANTLE_ALARM = 0x404; // host desmantle alarm
        public const int MINOR_HOST_DESMANTLE_RESUME = 0x405; //  host desmantle resume
        public const int MINOR_CARD_READER_DESMANTLE_ALARM = 0x406; // card reader desmantle alarm 
        public const int MINOR_CARD_READER_DESMANTLE_RESUME = 0x407; // card reader desmantle resume
        public const int MINOR_CASE_SENSOR_ALARM = 0x408; // case sensor alarm 
        public const int MINOR_CASE_SENSOR_RESUME = 0x409; // case sensor resume 
        public const int MINOR_STRESS_ALARM = 0x40a; // stress alarm 
        public const int MINOR_OFFLINE_ECENT_NEARLY_FULL = 0x40b; // offline ecent nearly full 
        public const int MINOR_CARD_MAX_AUTHENTICATE_FAIL = 0x40c; // card max authenticate fall 
        public const int MINOR_SD_CARD_FULL = 0x40d; // SD card is full
        public const int MINOR_LINKAGE_CAPTURE_PIC = 0x40e; // lingage capture picture
        public const int MINOR_SECURITY_MODULE_DESMANTLE_ALARM = 0x40f;  //Door control security module desmantle alarm
        public const int MINOR_SECURITY_MODULE_DESMANTLE_RESUME = 0x410;  //Door control security module desmantle resume
        public const int MINOR_POS_START_ALARM = 0x411; // POS Start
        public const int MINOR_POS_END_ALARM = 0x412; // POS end
        public const int MINOR_FACE_IMAGE_QUALITY_LOW = 0x413; // face image quality low
        public const int MINOR_FINGE_RPRINT_QUALITY_LOW = 0x414; // finger print quality low
        public const int MINOR_FIRE_IMPORT_SHORT_CIRCUIT = 0x415; // Fire import short circuit
        public const int MINOR_FIRE_IMPORT_BROKEN_CIRCUIT = 0x416; // Fire import broken circuit
        public const int MINOR_FIRE_IMPORT_RESUME = 0x417; // Fire import resume
        public const int MINOR_FIRE_BUTTON_TRIGGER = 0x418; // fire button trigger
        public const int MINOR_FIRE_BUTTON_RESUME = 0x419; // fire button resume
        public const int MINOR_MAINTENANCE_BUTTON_TRIGGER = 0x41a; // maintenance button trigger
        public const int MINOR_MAINTENANCE_BUTTON_RESUME = 0x41b; // maintenance button resume
        public const int MINOR_EMERGENCY_BUTTON_TRIGGER = 0x41c; // emergency button trigger
        public const int MINOR_EMERGENCY_BUTTON_RESUME = 0x41d; // emergency button resume
        public const int MINOR_DISTRACT_CONTROLLER_ALARM = 0x41e; // distract controller alarm
        public const int MINOR_DISTRACT_CONTROLLER_RESUME = 0x41f; // distract controller resume
        public const int MINOR_CHANNEL_CONTROLLER_DESMANTLE_ALARM = 0x422; //channel controller desmantle alarm
        public const int MINOR_CHANNEL_CONTROLLER_DESMANTLE_RESUME = 0x423; //channel controller desmantle resume
        public const int MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_ALARM = 0x424; //channel controller fire import alarm
        public const int MINOR_CHANNEL_CONTROLLER_FIRE_IMPORT_RESUME = 0x425;  //channel controller fire import resume
        public const int MINOR_PRINTER_OUT_OF_PAPER = 0x440;  //printer no paper
        public const int MINOR_LEGAL_EVENT_NEARLY_FULL = 0x442;  //Legal event nearly full

        /* Exception*/
        // Main Type
        public const int MAJOR_EXCEPTION = 0x2;
        // Hypo- Type

        public const int MINOR_NET_BROKEN = 0x27; // Network disconnected 
        public const int MINOR_RS485_DEVICE_ABNORMAL = 0x3a; // RS485 connect status exception
        public const int MINOR_RS485_DEVICE_REVERT = 0x3b; // RS485 connect status exception recovery

        public const int MINOR_DEV_POWER_ON = 0x400; // device power on
        public const int MINOR_DEV_POWER_OFF = 0x401; // device power off
        public const int MINOR_WATCH_DOG_RESET = 0x402; // watch dog reset 
        public const int MINOR_LOW_BATTERY = 0x403; // low battery 
        public const int MINOR_BATTERY_RESUME = 0x404; // battery resume
        public const int MINOR_AC_OFF = 0x405; // AC off
        public const int MINOR_AC_RESUME = 0x406; // AC resume 
        public const int MINOR_NET_RESUME = 0x407; // Net resume
        public const int MINOR_FLASH_ABNORMAL = 0x408; // FLASH abnormal 
        public const int MINOR_CARD_READER_OFFLINE = 0x409; // card reader offline 
        public const int MINOR_CARD_READER_RESUME = 0x40a; // card reader resume 
        public const int MINOR_INDICATOR_LIGHT_OFF = 0x40b; // Indicator Light Off
        public const int MINOR_INDICATOR_LIGHT_RESUME = 0x40c; // Indicator Light Resume
        public const int MINOR_CHANNEL_CONTROLLER_OFF = 0x40d; // channel controller off
        public const int MINOR_CHANNEL_CONTROLLER_RESUME = 0x40e; // channel controller resume
        public const int MINOR_SECURITY_MODULE_OFF = 0x40f; // Door control security module off
        public const int MINOR_SECURITY_MODULE_RESUME = 0x410; // Door control security module resume
        public const int MINOR_BATTERY_ELECTRIC_LOW = 0x411; // battery electric low
        public const int MINOR_BATTERY_ELECTRIC_RESUME = 0x412; // battery electric resume
        public const int MINOR_LOCAL_CONTROL_NET_BROKEN = 0x413; // Local control net broken
        public const int MINOR_LOCAL_CONTROL_NET_RSUME = 0x414; // Local control net resume
        public const int MINOR_MASTER_RS485_LOOPNODE_BROKEN = 0x415; // Master RS485 loop node broken
        public const int MINOR_MASTER_RS485_LOOPNODE_RESUME = 0x416; // Master RS485 loop node resume
        public const int MINOR_LOCAL_CONTROL_OFFLINE = 0x417; // Local control offline
        public const int MINOR_LOCAL_CONTROL_RESUME = 0x418; // Local control resume
        public const int MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN = 0x419; // Local downside RS485 loop node broken
        public const int MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME = 0x41a; // Local downside RS485 loop node resume
        public const int MINOR_DISTRACT_CONTROLLER_ONLINE = 0x41b; // distract controller online
        public const int MINOR_DISTRACT_CONTROLLER_OFFLINE = 0x41c; // distract controller offline
        public const int MINOR_ID_CARD_READER_NOT_CONNECT = 0x41d; // Id card reader not connected(intelligent dedicated)
        public const int MINOR_ID_CARD_READER_RESUME = 0x41e; //Id card reader connection restored(intelligent dedicated)
        public const int MINOR_FINGER_PRINT_MODULE_NOT_CONNECT = 0x41f; // fingerprint module is not connected(intelligent dedicated)
        public const int MINOR_FINGER_PRINT_MODULE_RESUME = 0x420; // The fingerprint module connection restored(intelligent dedicated)
        public const int MINOR_CAMERA_NOT_CONNECT = 0x421; // Camera not connected
        public const int MINOR_CAMERA_RESUME = 0x422; // Camera connection restored
        public const int MINOR_COM_NOT_CONNECT = 0x423; // COM not connected
        public const int MINOR_COM_RESUME = 0x424;// COM connection restored
        public const int MINOR_DEVICE_NOT_AUTHORIZE = 0x425; // device are not authorized
        public const int MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE = 0x426; // people and ID card device online
        public const int MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE = 0x427;// people and ID card device offline
        public const int MINOR_LOCAL_LOGIN_LOCK = 0x428; // local login lock
        public const int MINOR_LOCAL_LOGIN_UNLOCK = 0x429; //local login unlock
        public const int MINOR_SUBMARINEBACK_COMM_BREAK = 0x42a;  //submarineback communicate break
        public const int MINOR_SUBMARINEBACK_COMM_RESUME = 0x42b;  //submarineback communicate resume
        public const int MINOR_MOTOR_SENSOR_EXCEPTION = 0x42c;  //motor sensor exception
        public const int MINOR_CAN_BUS_EXCEPTION = 0x42d;  //can bus exception
        public const int MINOR_CAN_BUS_RESUME = 0x42e;  //can bus resume
        public const int MINOR_GATE_TEMPERATURE_OVERRUN = 0x42f; //gate temperature over run
        public const int MINOR_IR_EMITTER_EXCEPTION = 0x430; //IR emitter exception
        public const int MINOR_IR_EMITTER_RESUME = 0x431;  //IR emitter resume
        public const int MINOR_LAMP_BOARD_COMM_EXCEPTION = 0x432;  //lamp board communicate exception
        public const int MINOR_LAMP_BOARD_COMM_RESUME = 0x433;  //lamp board communicate resume
        public const int MINOR_IR_ADAPTOR_COMM_EXCEPTION = 0x434; //IR adaptor communicate exception
        public const int MINOR_IR_ADAPTOR_COMM_RESUME = 0x435;  //IR adaptor communicate resume
        public const int MINOR_PRINTER_ONLINE = 0x436; //printer online
        public const int MINOR_PRINTER_OFFLINE = 0x437; //printer offline
        public const int MINOR_4G_MOUDLE_ONLINE = 0x438; //4G module online
        public const int MINOR_4G_MOUDLE_OFFLINE = 0x439; //4G module offline


        /* Operation  */
        // Main Type
        public const int MAJOR_OPERATION = 0x3;

        // Hypo- Type
        public const int MINOR_LOCAL_UPGRADE = 0x5a; // Upgrade  (local)
        public const int MINOR_REMOTE_LOGIN = 0x70; // Login  (remote)
        public const int MINOR_REMOTE_LOGOUT = 0x71; // Logout   (remote)
        public const int MINOR_REMOTE_ARM = 0x79; // On guard   (remote)
        public const int MINOR_REMOTE_DISARM = 0x7a; // Disarm   (remote)
        public const int MINOR_REMOTE_REBOOT = 0x7b; // Reboot   (remote)
        public const int MINOR_REMOTE_UPGRADE = 0x7e; // upgrade  (remote)
        public const int MINOR_REMOTE_CFGFILE_OUTPUT = 0x86; // Export Configuration   (remote) 
        public const int MINOR_REMOTE_CFGFILE_INTPUT = 0x87; // Import Configuration  (remote) 
        public const int MINOR_REMOTE_ALARMOUT_OPEN_MAN = 0xd6; // remote mamual open alarmout 
        public const int MINOR_REMOTE_ALARMOUT_CLOSE_MAN = 0xd7; // remote mamual close alarmout 

        public const int MINOR_REMOTE_OPEN_DOOR = 0x400; // remote open door 
        public const int MINOR_REMOTE_CLOSE_DOOR = 0x401; // remote close door (controlled) 
        public const int MINOR_REMOTE_ALWAYS_OPEN = 0x402; // remote always open door (free) 
        public const int MINOR_REMOTE_ALWAYS_CLOSE = 0x403; // remote always close door (forbiden)
        public const int MINOR_REMOTE_CHECK_TIME = 0x404; // remote check time 
        public const int MINOR_NTP_CHECK_TIME = 0x405; // ntp check time 
        public const int MINOR_REMOTE_CLEAR_CARD = 0x406; // remote clear card 
        public const int MINOR_REMOTE_RESTORE_CFG = 0x407; // remote restore configure 
        public const int MINOR_ALARMIN_ARM = 0x408; // alarm in arm 
        public const int MINOR_ALARMIN_DISARM = 0x409; // alarm in disarm 
        public const int MINOR_LOCAL_RESTORE_CFG = 0x40a; // local configure restore 
        public const int MINOR_REMOTE_CAPTURE_PIC = 0x40b; // remote capture picture 
        public const int MINOR_MOD_NET_REPORT_CFG = 0x40c; // modify net report cfg 
        public const int MINOR_MOD_GPRS_REPORT_PARAM = 0x40d; // modify GPRS report param 
        public const int MINOR_MOD_REPORT_GROUP_PARAM = 0x40e; // modify report group param 
        public const int MINOR_UNLOCK_PASSWORD_OPEN_DOOR = 0x40f; // unlock password open door 
        public const int MINOR_AUTO_RENUMBER = 0x410; // auto renumber 
        public const int MINOR_AUTO_COMPLEMENT_NUMBER = 0x411; // auto complement number 
        public const int MINOR_NORMAL_CFGFILE_INPUT = 0x412; // normal cfg file input 
        public const int MINOR_NORMAL_CFGFILE_OUTTPUT = 0x413; // normal cfg file output 
        public const int MINOR_CARD_RIGHT_INPUT = 0x414; // card right input 
        public const int MINOR_CARD_RIGHT_OUTTPUT = 0x415; // card right output 
        public const int MINOR_LOCAL_USB_UPGRADE = 0x416; // local USB upgrade 
        public const int MINOR_REMOTE_VISITOR_CALL_LADDER = 0x417; // visitor call ladder 
        public const int MINOR_REMOTE_HOUSEHOLD_CALL_LADDER = 0x418; // household call ladder 
        public const int MINOR_REMOTE_ACTUAL_GUARD = 0x419;  //remote actual guard
        public const int MINOR_REMOTE_ACTUAL_UNGUARD = 0x41a;  //remote actual unguard
        public const int MINOR_REMOTE_CONTROL_NOT_CODE_OPER_FAILED = 0x41b; //remote control not code operate failed
        public const int MINOR_REMOTE_CONTROL_CLOSE_DOOR = 0x41c; //remote control close door
        public const int MINOR_REMOTE_CONTROL_OPEN_DOOR = 0x41d; //remote control open door
        public const int MINOR_REMOTE_CONTROL_ALWAYS_OPEN_DOOR = 0x41e; //remote control always open door

        /* Additional Log Info*/
        // Main Type
        public const int MAJOR_EVENT = 0x5;/*event*/
        // Hypo- Type
        public const int MINOR_LEGAL_CARD_PASS = 0x01; // legal card pass
        public const int MINOR_CARD_AND_PSW_PASS = 0x02; // swipe and password pass
        public const int MINOR_CARD_AND_PSW_FAIL = 0x03; // swipe and password fail
        public const int MINOR_CARD_AND_PSW_TIMEOUT = 0x04; // swipe and password timeout
        public const int MINOR_CARD_AND_PSW_OVER_TIME = 0x05; // swipe and password over time
        public const int MINOR_CARD_NO_RIGHT = 0x06; // card no right 
        public const int MINOR_CARD_INVALID_PERIOD = 0x07; // invalid period 
        public const int MINOR_CARD_OUT_OF_DATE = 0x08; // card out of date
        public const int MINOR_INVALID_CARD = 0x09; // invalid card 
        public const int MINOR_ANTI_SNEAK_FAIL = 0x0a; // anti sneak fail 
        public const int MINOR_INTERLOCK_DOOR_NOT_CLOSE = 0x0b; // interlock door doesn't close
        public const int MINOR_NOT_BELONG_MULTI_GROUP = 0x0c; // card no belong multi group 
        public const int MINOR_INVALID_MULTI_VERIFY_PERIOD = 0x0d; // invalid multi verify period 
        public const int MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL = 0x0e; // have no super right in multi verify mode 
        public const int MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL = 0x0f; // have no remote right in multi verify mode 
        public const int MINOR_MULTI_VERIFY_SUCCESS = 0x10; // success in multi verify mode 
        public const int MINOR_LEADER_CARD_OPEN_BEGIN = 0x11; // leader card begin to open
        public const int MINOR_LEADER_CARD_OPEN_END = 0x12; // leader card end to open 
        public const int MINOR_ALWAYS_OPEN_BEGIN = 0x13; // always open begin
        public const int MINOR_ALWAYS_OPEN_END = 0x14; // always open end
        public const int MINOR_LOCK_OPEN = 0x15; // lock open
        public const int MINOR_LOCK_CLOSE = 0x16; // lock close
        public const int MINOR_DOOR_BUTTON_PRESS = 0x17; // press door open button 
        public const int MINOR_DOOR_BUTTON_RELEASE = 0x18; // release door open button 
        public const int MINOR_DOOR_OPEN_NORMAL = 0x19; // door open normal 
        public const int MINOR_DOOR_CLOSE_NORMAL = 0x1a; // door close normal 
        public const int MINOR_DOOR_OPEN_ABNORMAL = 0x1b; // open door abnormal 
        public const int MINOR_DOOR_OPEN_TIMEOUT = 0x1c; // open door timeout 
        public const int MINOR_ALARMOUT_ON = 0x1d; // alarm out turn on 
        public const int MINOR_ALARMOUT_OFF = 0x1e; // alarm out turn off 
        public const int MINOR_ALWAYS_CLOSE_BEGIN = 0x1f; // always close begin 
        public const int MINOR_ALWAYS_CLOSE_END = 0x20; // always close end 
        public const int MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN = 0x21; // need remote open in multi verify mode 
        public const int MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS = 0x22; // superpasswd verify success in multi verify mode 
        public const int MINOR_MULTI_VERIFY_REPEAT_VERIFY = 0x23; // repeat verify in multi verify mode 
        public const int MINOR_MULTI_VERIFY_TIMEOUT = 0x24; // timeout in multi verify mode 
        public const int MINOR_DOORBELL_RINGING = 0x25; // doorbell ringing 
        public const int MINOR_FINGERPRINT_COMPARE_PASS = 0x26; // fingerprint compare pass 
        public const int MINOR_FINGERPRINT_COMPARE_FAIL = 0x27; // fingerprint compare fail 
        public const int MINOR_CARD_FINGERPRINT_VERIFY_PASS = 0x28; // card and fingerprint verify pass 
        public const int MINOR_CARD_FINGERPRINT_VERIFY_FAIL = 0x29; // card and fingerprint verify fail 
        public const int MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT = 0x2a; // card and fingerprint verify timeout 
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2b; // card and fingerprint and passwd verify pass 
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2c; // card and fingerprint and passwd verify fail 
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x2d; // card and fingerprint and passwd verify timeout 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2e; // fingerprint and passwd verify pass 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2f; // fingerprint and passwd verify fail 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x30; // fingerprint and passwd verify timeout 
        public const int MINOR_FINGERPRINT_INEXISTENCE = 0x31; // fingerprint inexistence 
        public const int MINOR_CARD_PLATFORM_VERIFY = 0x32; // card platform verify 
        public const int MINOR_CALL_CENTER = 0x33; // call center 
        public const int MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN = 0x34; // fire relay turn on door always open 
        public const int MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL = 0x35; // fire relay recover door recover normal 
        public const int MINOR_FACE_AND_FP_VERIFY_PASS = 0x36; // face and finger print verify pass 
        public const int MINOR_FACE_AND_FP_VERIFY_FAIL = 0x37; // face and finger print verify fail 
        public const int MINOR_FACE_AND_FP_VERIFY_TIMEOUT = 0x38; // face and finger print verify timeout 
        public const int MINOR_FACE_AND_PW_VERIFY_PASS = 0x39; // face and password verify pass 
        public const int MINOR_FACE_AND_PW_VERIFY_FAIL = 0x3a; // face and password verify fail 
        public const int MINOR_FACE_AND_PW_VERIFY_TIMEOUT = 0x3b; // face and password verify timeout 
        public const int MINOR_FACE_AND_CARD_VERIFY_PASS = 0x3c; // face and card verify pass 
        public const int MINOR_FACE_AND_CARD_VERIFY_FAIL = 0x3d; // face and card verify fail 
        public const int MINOR_FACE_AND_CARD_VERIFY_TIMEOUT = 0x3e; // face and card verify timeout 
        public const int MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS = 0x3f; // face and password and finger print verify pass 
        public const int MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL = 0x40; // face and password and finger print verify fail 
        public const int MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT = 0x41; // face and password and finger print verify timeout 
        public const int MINOR_FACE_CARD_AND_FP_VERIFY_PASS = 0x42; // face and card and finger print verify pass 
        public const int MINOR_FACE_CARD_AND_FP_VERIFY_FAIL = 0x43; // face and card and finger print verify fail 
        public const int MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT = 0x44; // face and card and finger print verify timeout 
        public const int MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS = 0x45; // employee and finger print verify pass 
        public const int MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL = 0x46; // employee and finger print verify fail 
        public const int MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT = 0x47; // employee and finger print verify timeout 
        public const int MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS = 0x48; // employee and finger print and password verify pass 
        public const int MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL = 0x49; // employee and finger print and password verify fail 
        public const int MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT = 0x4a; // employee and finger print and password verify timeout
        public const int MINOR_FACE_VERIFY_PASS = 0x4b; // face verify pass 
        public const int MINOR_FACE_VERIFY_FAIL = 0x4c; // face verify fail 
        public const int MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS = 0x4d; // employee no and face verify pass 
        public const int MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL = 0x4e; // employee no and face verify fail 
        public const int MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT = 0x4f; // employee no and face verify time out 
        public const int MINOR_FACE_RECOGNIZE_FAIL = 0x50; // face recognize fail 
        public const int MINOR_FIRSTCARD_AUTHORIZE_BEGIN = 0x51; // first card authorize begin 
        public const int MINOR_FIRSTCARD_AUTHORIZE_END = 0x52; // first card authorize end 
        public const int MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT = 0x53; // door lock input short circuit 
        public const int MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT = 0x54; // door lock input broken circuit 
        public const int MINOR_DOORLOCK_INPUT_EXCEPTION = 0x55; // door lock input exception 
        public const int MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT = 0x56; // door contact input short circuit 
        public const int MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT = 0x57; // door contact input broken circuit 
        public const int MINOR_DOORCONTACT_INPUT_EXCEPTION = 0x58; // door contact input exception 
        public const int MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT = 0x59; // open button input short circuit 
        public const int MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT = 0x5a; // open button input broken circuit 
        public const int MINOR_OPENBUTTON_INPUT_EXCEPTION = 0x5b; // open button input exception 
        public const int MINOR_DOORLOCK_OPEN_EXCEPTION = 0x5c; // door lock open exception 
        public const int MINOR_DOORLOCK_OPEN_TIMEOUT = 0x5d; // door lock open timeout 
        public const int MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE = 0x5e; // first card open without authorize 
        public const int MINOR_CALL_LADDER_RELAY_BREAK = 0x5f; // call ladder relay break 
        public const int MINOR_CALL_LADDER_RELAY_CLOSE = 0x60; // call ladder relay close 
        public const int MINOR_AUTO_KEY_RELAY_BREAK = 0x61; // auto key relay break 
        public const int MINOR_AUTO_KEY_RELAY_CLOSE = 0x62; // auto key relay close 
        public const int MINOR_KEY_CONTROL_RELAY_BREAK = 0x63; // key control relay break 
        public const int MINOR_KEY_CONTROL_RELAY_CLOSE = 0x64; // key control relay close 
        public const int MINOR_EMPLOYEENO_AND_PW_PASS = 0x65; // minor employee no and password pass 
        public const int MINOR_EMPLOYEENO_AND_PW_FAIL = 0x66; // minor employee no and password fail 
        public const int MINOR_EMPLOYEENO_AND_PW_TIMEOUT = 0x67; // minor employee no and password timeout 
        public const int MINOR_HUMAN_DETECT_FAIL = 0x68; // human detect fail 
        public const int MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS = 0x69; // the comparison with people and id card success 
        public const int MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL = 0x70; // the comparison with people and id card failed 
        public const int MINOR_CERTIFICATE_BLOCK_LIST = 0x71;
        public const int MINOR_LEGAL_MESSAGE = 0x72; // legal message 
        public const int MINOR_ILLEGAL_MESSAGE = 0x73; // illegal messag 
        public const int MINOR_MAC_DETECT = 0x74; // mac detect 
        public const int MINOR_DOOR_OPEN_OR_DORMANT_FAIL = 0x75; //door open or dormant fail
        public const int MINOR_AUTH_PLAN_DORMANT_FAIL = 0x76;  //auth plan dormant fail
        public const int MINOR_CARD_ENCRYPT_VERIFY_FAIL = 0x77; //card encrypt verify fail
        public const int MINOR_SUBMARINEBACK_REPLY_FAIL = 0x78;  //submarineback reply fail
        public const int MINOR_DOOR_OPEN_OR_DORMANT_OPEN_FAIL = 0x82;  //door open or dormant open fail
        public const int MINOR_DOOR_OPEN_OR_DORMANT_LINKAGE_OPEN_FAIL = 0x84; //door open or dormant linkage open fail
        public const int MINOR_TRAILING = 0x85;  //trailing
        public const int MINOR_HEART_BEAT = 0x83;  //heart beat event
        public const int MINOR_REVERSE_ACCESS = 0x86; //reverse access
        public const int MINOR_FORCE_ACCESS = 0x87; //force access
        public const int MINOR_CLIMBING_OVER_GATE = 0x88; //climbing over gate
        public const int MINOR_PASSING_TIMEOUT = 0x89;  //passing timeout
        public const int MINOR_INTRUSION_ALARM = 0x8a;  //intrusion alarm
        public const int MINOR_FREE_GATE_PASS_NOT_AUTH = 0x8b; //free gate pass not auth
        public const int MINOR_DROP_ARM_BLOCK = 0x8c; //drop arm block
        public const int MINOR_DROP_ARM_BLOCK_RESUME = 0x8d;  //drop arm block resume
        public const int MINOR_LOCAL_FACE_MODELING_FAIL = 0x8e;  //device upgrade with module failed
        public const int MINOR_STAY_EVENT = 0x8f;  //stay event
        public const int MINOR_PASSWORD_MISMATCH = 0x97;  //password mismatch
        public const int MINOR_EMPLOYEE_NO_NOT_EXIST = 0x98;  //employee no not exist
        public const int MINOR_COMBINED_VERIFY_PASS = 0x99;  //combined verify pass
        public const int MINOR_COMBINED_VERIFY_TIMEOUT = 0x9a;  //combined verify timeout
        public const int MINOR_VERIFY_MODE_MISMATCH = 0x9b;  //verify mode mismatch

        #endregion

        #region card parameters configuration
        public const int CARD_PARAM_CARD_VALID = 0x00000001;  //card valid parameter 
        public const int CARD_PARAM_VALID = 0x00000002;  //valid period parameter
        public const int CARD_PARAM_CARD_TYPE = 0x00000004;  //card type parameter
        public const int CARD_PARAM_DOOR_RIGHT = 0x00000008;  //door right parameter
        public const int CARD_PARAM_LEADER_CARD = 0x00000010;  //leader card parameter
        public const int CARD_PARAM_SWIPE_NUM = 0x00000020;  //max swipe time parameter
        public const int CARD_PARAM_GROUP = 0x00000040;  //belong group parameter
        public const int CARD_PARAM_PASSWORD = 0x00000080;  //card password parameter
        public const int CARD_PARAM_RIGHT_PLAN = 0x00000100;  //card right plan parameter
        public const int CARD_PARAM_SWIPED_NUM = 0x00000200;  //has swiped card time parameter
        public const int CARD_PARAM_EMPLOYEE_NO = 0x00000400;  //employee no


        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;   //max card right plan number
        public const int MAX_DOOR_CODE_LEN = 8; //room code length
        public const int MAX_LOCK_CODE_LEN = 8; //lock code length

        public const int MAX_CASE_SENSOR_NUM = 8;   //max case sensor number
        public const int MAX_ALARMHOST_ALARMIN_NUM = 512; //Max number of alarm host alarm input ports
        public const int MAX_ALARMHOST_ALARMOUT_NUM = 512; //Max number of alarm host alarm output ports

        public const int NET_DVR_GET_ACS_WORK_STATUS_V50 = 2180;   //Access door host working condition (V50)
        public const int NET_DVR_GET_CARD_CFG_V50 = 2178;    //Parameters to acquire new CARDS (V50)
        public const int NET_DVR_SET_CARD_CFG_V50 = 2179;    //Setting up the new parameters (V50)

        #endregion

        #endregion

        #region HCNetSDK.dll structure definition

        #region definicion estructuras facial
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;    //serial number
            public byte byAlarmInPortNum;   //Number of Alarm input
            public byte byAlarmOutPortNum;  //Number of Alarm Output
            public byte byDiskNum;  //Number of Hard Disk
            public byte byDVRType;  //DVR Type, 1: DVR 2: ATM DVR 3: DVS ......
            public byte byChanNum;  //Number of Analog Channel
            public byte byStartChan;    //The first Channel No. E.g. DVS- 1, DVR- 1
            public byte byAudioChanNum; //Number of Audio Channel
            public byte byIPChanNum;    //Maximum number of IP Channel  low
            public byte byZeroChanNum;  //Zero channel encoding number//2010- 01- 16
            public byte byMainProto;    //Main stream transmission protocol 0- private,  1- rtsp,2-both private and rtsp
            public byte bySubProto; //Sub stream transmission protocol 0- private,  1- rtsp,2-both private and rtsp
            public byte bySupport;  //Ability, the 'AND' result by bit: 0- not support;  1- support
            //bySupport & 0x1,  smart search
            //bySupport & 0x2,  backup
            //bySupport & 0x4,  get compression configuration ability
            //bySupport & 0x8,  multi network adapter
            //bySupport & 0x10, support remote SADP
            //bySupport & 0x20  support Raid card
            //bySupport & 0x40 support IPSAN directory search
            public byte bySupport1; //Ability expand, the 'AND' result by bit: 0- not support;  1- support
            //bySupport1 & 0x1, support snmp v30
            //bySupport1& 0x2,support distinguish download and playback
            //bySupport1 & 0x4, support deployment level
            //bySupport1 & 0x8, support vca alarm time extension 
            //bySupport1 & 0x10, support muti disks(more than 33)
            //bySupport1 & 0x20, support rtsp over http
            //bySupport1 & 0x40, support delay preview
            //bySuppory1 & 0x80 support NET_DVR_IPPARACFG_V40, in addition  support  License plate of the new alarm information
            public byte bySupport2; //Ability expand, the 'AND' result by bit: 0- not support;  1- support
            //bySupport & 0x1, decoder support get stream by URL
            //bySupport2 & 0x2,  support FTPV40
            //bySupport2 & 0x4,  support ANR
            //bySupport2 & 0x20, support get single item of device status
            //bySupport2 & 0x40,  support stream encryt
            public ushort wDevType; //device type
            public byte bySupport3; //Support  epresent by bit, 0 - not support 1 - support 
            //bySupport3 & 0x1-muti stream support 
            //bySupport3 & 0x8  support use delay preview parameter when delay preview
            //bySupport3 & 0x10 support the interface of getting alarmhost main status V40
            public byte byMultiStreamProto; //support multi stream, represent by bit, 0-not support ;1- support; bit1-stream 3 ;bit2-stream 4, bit7-main stream, bit8-sub stream
            public byte byStartDChan;   //Start digital channel
            public byte byStartDTalkChan;   //Start digital talk channel
            public byte byHighDChanNum; //Digital channel number high
            public byte bySupport4; //Support  epresent by bit, 0 - not support 1 - support
            //bySupport4 & 0x4 whether support video wall unified interface
            // bySupport4 & 0x80 Support device upload center alarm enable
            public byte byLanguageType; //support language type by bit,0-support,1-not support  
            //byLanguageType 0 -old device
            //byLanguageType & 0x1 support chinese
            //byLanguageType & 0x2 support english
            public byte byVoiceInChanNum;   //voice in chan num
            public byte byStartVoiceInChanNo;   //start voice in chan num
            public byte bySupport5;  //0-no support,1-support,bit0-muti stream
            //bySupport5 &0x01support wEventTypeEx 
            //bySupport5 &0x04support sence expend
            public byte bySupport6;
            public byte byMirrorChanNum;    //mirror channel num,<it represents direct channel in the recording host
            public ushort wStartMirrorChanNo;   //start mirror chan
            public byte bySupport7;        //Support  epresent by bit, 0 - not support 1 - support 
            //bySupport7 & 0x1- supports INTER_VCA_RULECFG_V42 extension    
            // bySupport7 & 0x2  Supports HVT IPC mode expansion
            // bySupport7 & 0x04  Back lock time
            // bySupport7 & 0x08  Set the pan PTZ position, whether to support the band channel
            // bySupport7 & 0x10  Support for dual system upgrade backup
            // bySupport7 & 0x20  Support OSD character overlay V50
            // bySupport7 & 0x40  Support master slave tracking (slave camera)
            // bySupport7 & 0x80  Support message encryption 
            public byte byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_COND
        {
            public int dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN)]
            public byte[] byCardNo;//人脸关联的卡号（设置时该参数可不设置）
            public int dwFaceNum;// 设置或获取人脸数量，获取时置为0xffffffff表示获取所有人脸信息
            public int dwEnableReaderNo;// 人脸读卡器编号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124)]
            public byte[] byRes;
            public void Init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byRes = new byte[124];
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_RECORD
        {
            public int dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN)]
            public byte[] byCardNo;
            public int dwFaceLen;
            public IntPtr pFaceBuffer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] byRes;

            public void Init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byRes = new byte[178];
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            public byte bySupportLock;        //设备支持锁定功能，该字段由SDK根据设备返回值来赋值的。bySupportLock为1时，dwSurplusLockTime和byRetryLoginTime有效
            public byte byRetryLoginTime;        //剩余可尝试登陆的次数，用户名，密码错误时，此参数有效
            public byte byPasswordLevel;      //admin密码安全等级0-无效，1-默认密码，2-有效密码,3-风险较高的密码。当用户的密码为出厂默认密码（12345）或者风险较高的密码时，上层客户端需要提示用户更改密码。      
            public byte byProxyType;  //代理类型，0-不使用代理, 1-使用socks5代理, 2-使用EHome代理
            public uint dwSurplusLockTime;    //剩余时间，单位秒，用户锁定时，此参数有效
            public byte byCharEncodeType;     //字符编码类型
            public byte bySupportDev5;//支持v50版本的设备参数获取，设备名称和设备类型名称长度扩展为64字节
            public byte bySupport;  //能力集扩展，位与结果：0- 不支持，1- 支持
            // bySupport & 0x1:  保留
            // bySupport & 0x2:  0-不支持变化上报 1-支持变化上报
            public byte byLoginMode; //登录模式 0-Private登录 1-ISAPI登录
            public uint dwOEMCode;
            public int iResidualValidity;   //该用户密码剩余有效天数，单位：天，返回负值，表示密码已经超期使用，例如“-3表示密码已经超期使用3天”
            public byte byResidualValidity; // iResidualValidity字段是否有效，0-无效，1-有效
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 243)]
            public byte[] byRes2;
        }

        public delegate void LoginResultCallBack(int lUserID, uint dwResult, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo, IntPtr pUser);
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_LOGIN_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_DEV_ADDRESS_MAX_LEN)]
            public string sDeviceAddress;
            public byte byUseTransport;
            public ushort wPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_USERNAME_MAX_LEN)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_PASSWD_MAX_LEN)]
            public string sPassword;
            public LoginResultCallBack cbLoginResult;
            public IntPtr pUser;
            public bool bUseAsynLogin;
            public byte byProxyType;
            public byte byUseUTCTime;
            public byte byLoginMode; //登录模式 0-Private 1-ISAPI 2-自适应（默认不采用自适应是因为自适应登录时，会对性能有较大影响，自适应时要同时发起ISAPI和Private登录）
            public byte byHttps;    //ISAPI登录时，是否使用HTTPS，0-不使用HTTPS，1-使用HTTPS 2-自适应（默认不采用自适应是因为自适应登录时，会对性能有较大影响，自适应时要同时发起HTTP和HTTPS）
            public int iProxyID;
            public byte byVerifyMode;  //认证方式，0-不认证，1-双向认证，2-单向认证；认证仅在使用TLS的时候生效;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 119, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_STATUS
        {
            public int dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ERROR_MSG_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byErrorMsg;//下发错误信息，当byCardReaderRecvStatus为4时，表示已存在人脸对应的卡号
            public int dwReaderNo; //人脸读卡器编号，可用于下发错误返回
            public byte byRecvStatus;  //人脸读卡器状态，按字节表示，0-失败，1-成功，2-重试或人脸质量差，3-内存已满(人脸数据满)，4-已存在该人脸，5-非法人脸ID
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 131, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byErrorMsg = new byte[Hik_SDK.ERROR_MSG_LEN];
                byRes = new byte[131];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CAPTURE_FACE_COND
        {
            public int dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes = new byte[128];
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CAPTURE_FACE_CFG
        {
            public int dwSize;
            public int dwFaceTemplate1Size;//人脸模板1数据大小，等于0时，代表无人脸模板1数据
            public IntPtr pFaceTemplate1Buffer;//人脸模板1数据缓存（不大于2.5k）
            public int dwFaceTemplate2Size;//人脸模板2数据大小，等于0时，代表无人脸模板2数据
            public IntPtr pFaceTemplate2Buffer; //人脸模板2数据缓存（不大于2.5K）
            public int dwFacePicSize;//人脸图片数据大小，等于0时，代表无人脸图片数据;
            public IntPtr pFacePicBuffer;//人脸图片数据缓存;
            public byte byFaceQuality1;//人脸质量，范围1-100
            public byte byFaceQuality2;//人脸质量，范围1-100
            public byte byCaptureProgress;    //采集进度，目前只有两种进度值：0-未采集到人脸，100-采集到人脸（只有在进度为100时，才解析人脸信息）
            public byte byRes1;
            public int dwInfraredFacePicSize;   //红外人脸图片数据大小，等于0时，代表无人脸图片数据
            public IntPtr pInfraredFacePicBuffer;      //红外人脸图片数据缓存
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes = new byte[116];
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_PARAM_CTRL_CARDNO
        {
            public int dwSize;
            public byte byMode;//删除方式，0-按卡号方式删除，1-按读卡器删除
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public Hik_SDK.NET_DVR_FACE_PARAM_BYCARD struByCard;//按卡号的方式删除,读卡器暂时不写
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                byRes1 = new byte[3];
                byRes = new byte[64];
                struByCard = new NET_DVR_FACE_PARAM_BYCARD();
                struByCard.Init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_PARAM_BYCARD
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //人脸关联的卡号
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.MAX_CARD_READER_NUM_512, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCardReader;//人脸的读卡器信息，按数组表示
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hik_SDK.MAX_FACE_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byFaceID; //需要删除的人脸编号，按数组下标，值表示0-不删除，1-删除该人脸
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 42, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;

            public void Init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byEnableCardReader = new byte[Hik_SDK.MAX_CARD_READER_NUM_512];
                byFaceID = new byte[Hik_SDK.MAX_FACE_NUM];
                byRes1 = new byte[42];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME
        {
            public int dwYear;
            public int dwMonth;
            public int dwDay;
            public int dwHour;
            public int dwMinute;
            public int dwSecond;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_EVENT_COND
        {
            public uint dwSize;
            public uint dwMajor;
            public uint dwMinor;
            public Hik_SDK.NET_DVR_TIME struStartTime;
            public Hik_SDK.NET_DVR_TIME struEndTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName;
            public uint dwBeginSerialNo;
            public byte byPicEnable;
            public byte byTimeType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwEndSerialNo;
            public uint dwIOTChannelNo;
            public ushort wInductiveEventType;
            public byte bySearchType;
            public byte byRes1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Hik_SDK.NET_SDK_MONITOR_ID_LEN)]
            public string szMonitorID;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.NET_SDK_EMPLOYEE_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byEmployeeNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 140, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byName = new byte[Hik_SDK.NAME_LEN];
                byRes2 = new byte[2];
                byEmployeeNo = new byte[Hik_SDK.NET_SDK_EMPLOYEE_NO_LEN];
                byRes = new byte[140];
            }
        }



        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_EVENT_CFG
        {
            public uint dwSize;
            public uint dwMajor;
            public uint dwMinor;
            public Hik_SDK.NET_DVR_TIME struTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.MAX_NAMELEN)]
            public byte[] sNetUser;
            public Hik_SDK.NET_DVR_IPADDR struRemoteHostAddr;
            public Hik_SDK.NET_DVR_ACS_EVENT_DETAIL struAcsEventInfo;
            public uint dwPicDataLen;
            public IntPtr pPicData;  // picture data
            public ushort wInductiveEventType;
            public byte byTimeType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 61)]
            public byte[] byRes;

            public void Init()
            {
                sNetUser = new byte[Hik_SDK.MAX_NAMELEN];
                struRemoteHostAddr.Init();
                struAcsEventInfo.init();
                byRes = new byte[61];
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPADDR
        {

            /// char[16]
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpV4;

            /// BYTE[128]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byIPv6;

            public void Init()
            {
                byIPv6 = new byte[128];
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_EVENT_DETAIL
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.ACS_CARD_NO_LEN)]
            public byte[] byCardNo;
            public byte byCardType;
            public byte byWhiteListNo;
            public byte byReportChannel;
            public byte byCardReaderKind;
            public uint dwCardReaderNo;
            public uint dwDoorNo;
            public uint dwVerifyNo;
            public uint dwAlarmInNo;
            public uint dwAlarmOutNo;
            public uint dwCaseSensorNo;
            public uint dwRs485No;
            public uint dwMultiCardGroupNo;
            public ushort wAccessChannel;//word
            public byte byDeviceNo;
            public byte byDistractControlNo;
            public uint dwEmployeeNo;
            public ushort wLocalControllerID;//word
            public byte byInternetAccess;
            public byte byType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.MACADDR_LEN)]
            public byte[] byMACAddr;
            public byte bySwipeCardType;
            public byte byRes2;
            public uint dwSerialNo;
            public byte byChannelControllerID;
            public byte byChannelControllerLampID;
            public byte byChannelControllerIRAdaptorID;
            public byte byChannelControllerIREmitterID;
            public uint dwRecordChannelNum;
            public IntPtr pRecordChannelData;
            public byte byUserType;
            public byte byCurrentVerifyMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRe2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hik_SDK.NET_SDK_EMPLOYEE_NO_LEN)]
            public byte[] byEmployeeNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
            public void init()
            {
                byCardNo = new byte[Hik_SDK.ACS_CARD_NO_LEN];
                byMACAddr = new byte[Hik_SDK.MACADDR_LEN];
                byRe2 = new byte[2];
                byEmployeeNo = new byte[Hik_SDK.NET_SDK_EMPLOYEE_NO_LEN];
                byRes = new byte[64];
            }
        }




        /*Estructuras Eventos*/

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG_V30
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;//Main type 1- alarm;  2- abnormal;  3- operation;  0xff- all 
            public uint dwMinorType; //Hypo- Type 0- all; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;//user ID for local panel operation
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;//user ID for network operation
            public NET_DVR_IPADDR struRemoteHostAddr;//remote host IP
            public uint dwParaType;//parameter type,  for 9000 series MINOR_START_VT/MINOR_STOP_VT,  channel of the voice talking
            public uint dwChannel;//channel number
            public uint dwDiskNumber;//HD number
            public uint dwAlarmInPort;//alarm input port
            public uint dwAlarmOutPort;//alarm output port
            public uint dwInfoLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN)]
            public string sInfo;
        }

        //  ACS event informations
        public struct NET_DVR_ACS_EVENT_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; // card No, 0 means invalid 
            public byte byCardType;
            public byte byWhiteListNo;  // white list No, 1-8, 0 means invalid
            public byte byReportChannel; // report channel, 1-alarmin updata, 2-center group 1, 3-center group 2, 0 means invalid
            public byte byCardReaderKind; // card reader type: 0-invalid, 1-IC card reader, 2-Id card reader, 3-Qr code reader, 4-Fingerprint head
            public uint dwCardReaderNo;  // card reader No, 0 means invalid
            public uint dwDoorNo;   // door No(floor No), 0 means invalid
            public uint dwVerifyNo;  // mutilcard verify No. 0 means invalid
            public uint dwAlarmInNo;  // alarm in No, 0 means invalid
            public uint dwAlarmOutNo;  // alarm out No 0 means invalid
            public uint dwCaseSensorNo;   // case sensor No 0 means invalid
            public uint dwRs485No;  // RS485 channel,0 means invalid
            public uint dwMultiCardGroupNo;  // multicard group No.
            public ushort wAccessChannel;      // Staff channel number 
            public byte byDeviceNo;  // device No,0 means invalid
            public byte byDistractControlNo;  // distract control,0 means invalid
            public uint dwEmployeeNo;   // employee No,0 means invalid
            public ushort wLocalControllerID; // On the controller number, 0 - access the host, 1-64 on behalf of the local controller 
            public byte byInternetAccess;  // Internet access ID (1-uplink network port 1, 2-uplink network port 2,3- downstream network interface 1
            public byte byType; // protection zone type, 0-real time, 1-24 hours, 2-delay, 3-internal, 4-the key, 5-fire, 6-perimeter, 7-24 hours of silent
            // 8-24 hours auxiliary, 9-24 hours vibration, 10-door emergency open, 11-door emergency shutdown, 0xff-null
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr; // mac addr 0 means invalid
            public byte bySwipeCardType;// swipe card type, 0-invalid,1-Qr code
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        // Entrance guard alarm information structure
        public struct NET_DVR_ACS_ALARM_INFO
        {
            public uint dwSize;
            public uint dwMajor;  // alarm major, reference to macro
            public uint dwMinor;  // alarm minor, reference to macro
            public NET_DVR_TIME struTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;  // net operator user
            public NET_DVR_IPADDR struRemoteHostAddr; // remote host address
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo;
            public uint dwPicDataLen; // picture length, when 0 ,means has no picture
            public IntPtr pPicData;  // picture data
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }


        //Alarm Device Infor
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            public byte byUserIDValid; /* Whether userID is valid,  0- invalid 1- valid. */
            public byte bySerialValid; /* Whether serial number is valid,  0- invalid 1- valid.  */
            public byte byVersionValid; /* Whether version number is valid,  0- invalid 1- valid. */
            public byte byDeviceNameValid; /* Whether device name is valid,  0- invalid 1- valid. */
            public byte byMacAddrValid; /* Whether MAC address is valid,  0- invalid 1- valid. */
            public byte byLinkPortValid; /* Whether login port number is valid,  0- invalid 1- valid. */
            public byte byDeviceIPValid; /* Whether device IP is valid,  0- invalid 1- valid.*/
            public byte bySocketIPValid; /* Whether socket IP is valid,  0- invalid 1- valid. */
            public int lUserID; /* NET_DVR_Login () effective when establishing alarm upload channel*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber; /* Serial number. */
            public uint dwDeviceVersion; /* Version number,  2 high byte means the major version,  2 low byte means the minor version*/
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName; /* Device name. */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr; /* MAC address */
            public ushort wLinkPort; /* link port */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP; /* IP address */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP; /* alarm push- mode socket IP address. */
            public byte byIpProtocol;  /* IP protocol:  0- IPV4;  1- IPV6. */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }


        //Alarm protection structure parameters
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM
        {
            public uint dwSize;
            public byte byLevel;  //Arming priority: 0-high, 1-middle, 2-low
            public byte byAlarmInfoType;//Upload alarm information types（Intelligent traffic camera support）：0- old（NET_DVR_PLATE_RESULT），1- new(NET_ITS_PLATE_RESULT)
            public byte byRetAlarmTypeV40; //0- Ret NET_DVR_ALARMINFO_V30 or Older, 1- if Device Support NET_DVR_ALARMINFO_V40,  Ret NET_DVR_ALARMINFO_V40, else Ret NET_DVR_ALARMINFO_V30 Or NET_DVR_ALARMINFO
            public byte byRetDevInfoVersion; //CVR alarm 0-COMM_ALARM_DEVICE, 1-COMM_ALARM_DEVICE_V40
            public byte byRetVQDAlarmType; //Exptected VQD alarm type,0-upload NET_DVR_VQD_DIAGNOSE_INFO,1-upload NET_DVR_VQD_ALARM
            //1-(INTER_FACE_DETECTION),0-(INTER_FACESNAP_RESULT)
            public byte byFaceAlarmDetection;
            //Bit0 - indicates whether the secondary protection to upload pictures: 0 - upload, 1 - do not upload 
            //Bit1 - said open data upload confirmation mechanism; 0 - don't open, 1 - to open
            public byte bySupport;
            //broken Net Http 
            //bit0-Vehicle Detection(IPC) (0 - not continuingly, 1 - continuingly)
            //bit1-PDC(IPC)  (0 - not continuingly, 1 - continuingly)
            //bit2-HeatMap(IPC)  (0 - not continuingly, 1 - continuingly)
            public byte byBrokenNetHttp;
            public ushort wTaskNo;//Tasking number and the (field dwTaskNo corresponding data upload NET_DVR_VEHICLE_RECOG_RESULT the same time issued a task structure NET_DVR_VEHICLE_RECOG_COND corresponding fields in dwTaskNo
            public byte byDeployType;//deploy type:0-client deploy,1-real time deploy
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byAlarmTypeURL;//bit0-(NET_DVR_FACESNAP_RESULT),0-binary,1-URL
            public byte byCustomCtrl;//Bit0- Support the copilot face picture upload: 0-Upload,1-Do not upload
        }

      


        #endregion

        #region definicion estructuras tarjetas

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_STATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //card No
            public uint dwErrorCode;
            public byte byStatus; //0-fail, 1-success
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byCardNo = new byte[ACS_CARD_NO_LEN];
                byRes = new byte[23];
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_SEND_DATA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //card No
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byCardNo = new byte[ACS_CARD_NO_LEN];
                byRes = new byte[16];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_EX
        {
            public ushort wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VALID_PERIOD_CFG
        {
            public byte byEnable; //whether to enable , 0-disable 1-enable
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struBeginTime; //valid begin time
            public NET_DVR_TIME_EX struEndTime; //valid end time 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_COND
        {
            public uint dwSize;
            public uint dwCardNum; //card number, 0xffffffff means to get all card information when getting
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[64];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_RECORD
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; //card No
            public byte byCardType;
            public byte byLeaderCard;
            public byte byUserType;
            public byte byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM_256, ArraySubType = UnmanagedType.I1)]
            public byte[] byDoorRight;
            public NET_DVR_VALID_PERIOD_CFG struValid;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_GROUP_NUM_128, ArraySubType = UnmanagedType.I1)]
            public byte[] byBelongGroup;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARD_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM_256, ArraySubType = UnmanagedType.I1)]
            public ushort[] wCardRightPlan;
            public uint dwMaxSwipeTimes;
            public uint dwSwipeTimes;
            public uint dwEmployeeNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byName;
            //按位表示，0-无权限，1-有权限
            //第0位表示：弱电报警
            //第1位表示：开门提示音
            //第2位表示：限制客卡
            //第3位表示：通道
            //第4位表示：反锁开门
            //第5位表示：巡更功能
            public uint dwCardRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byCardNo = new byte[ACS_CARD_NO_LEN];
                byDoorRight = new byte[MAX_DOOR_NUM_256];
                byBelongGroup = new byte[MAX_GROUP_NUM_128];
                byCardPassword = new byte[CARD_PASSWORD_LEN];
                wCardRightPlan = new ushort[MAX_DOOR_NUM_256];
                byName = new byte[NAME_LEN];
                byRes = new byte[256];
            }
        }
        #endregion


        #endregion

        #region  HCNetSDK.dll function definition

        // Obtiene el directorio base donde se ejecuta el programa
        //private static string directorioBase = AppDomain.CurrentDomain.BaseDirectory;

        // Define la ruta relativa a partir del directorio base
        private const string rutaLibreriaSDK = @"HCNetSDK\HCNetSDK.dll";

        //private const string rutaLibreriaSDK = @"HCNetSDK\HCNetSDK.dll";


        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_Init();

        [DllImport(rutaLibreriaSDK)]
        public static extern int NET_DVR_Login_V40(ref NET_DVR_USER_LOGIN_INFO pLoginInfo, ref NET_DVR_DEVICEINFO_V40 lpDeviceInfo);

        [DllImport(rutaLibreriaSDK)]
        public static extern uint NET_DVR_GetLastError();

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_SetLogToFile(int nLogLevel, string strLogDir, bool bAutoDel);


        public delegate void RemoteConfigCallback(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
        [DllImport(rutaLibreriaSDK)]
        public static extern int NET_DVR_StartRemoteConfig(int lUserID, int dwCommand, IntPtr lpInBuffer, int dwInBufferLen, RemoteConfigCallback cbStateCallback, IntPtr pUserData);

        // El entry point sirve para poder sobrecargar metodos que son importados de una libreria
        //En este caso tanto GNRCCFG y GNRCFR entran por getNextRemoteConfig pero tienen identificadores diferentes

        [DllImport(rutaLibreriaSDK, EntryPoint = "NET_DVR_GetNextRemoteConfig")]
        public static extern int NET_DVR_GetNextRemoteConfig_FaceCfg(int lHandle, ref Hik_SDK.NET_DVR_CAPTURE_FACE_CFG lpOutBuff, int dwOutBuffSize);

        [DllImport(rutaLibreriaSDK, EntryPoint = "NET_DVR_GetNextRemoteConfig")]
        public static extern int NET_DVR_GetNextRemoteConfig_FaceRecord(int lHandle, ref Hik_SDK.NET_DVR_FACE_RECORD lpOutBuff, int dwOutBuffSize);

        [DllImport(rutaLibreriaSDK, EntryPoint = "NET_DVR_GetNextRemoteConfig")]
        public static extern int NET_DVR_GetNextRemoteConfig_AcsEventCgf(int lHandle, ref Hik_SDK.NET_DVR_ACS_EVENT_CFG lpOutBuff, int dwOutBuffSize);

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_StopRemoteConfig(int lHandle);

        [DllImport(rutaLibreriaSDK, EntryPoint = "NET_DVR_SendWithRecvRemoteConfig")]
        public static extern int NET_DVR_SendWithRecvRemoteConfigFacial(int lHandle, ref Hik_SDK.NET_DVR_FACE_RECORD lpInBuff, int dwInBuffSize, ref Hik_SDK.NET_DVR_FACE_STATUS lpOutBuff, int dwOutBuffSize, IntPtr dwOutDataLen);

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_RemoteControl(int lUserID, int dwCommand, ref Hik_SDK.NET_DVR_FACE_PARAM_CTRL_CARDNO lpInBuffer, int dwInBufferSize);


        /*Prototipados puertas*/

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_ControlGateway(int lUserID, int lGatewayIndex, uint dwStaic);

        /*Prototipados Tarjetas*/
        [DllImport(rutaLibreriaSDK, EntryPoint = "NET_DVR_SendWithRecvRemoteConfig")]
        public static extern int NET_DVR_SendWithRecvRemoteConfigTarjeta(int lHandle, IntPtr lpInBuff, uint dwInBuffSize, IntPtr lpOutBuff, uint dwOutBuffSize, ref uint dwOutDataLen);

        /*Prototipados eventos*/


        [DllImport(rutaLibreriaSDK)]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID, ref NET_DVR_SETUPALARM_PARAM lpSetupParam);

        



        /*Prototipados general */

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_Logout_V30(Int32 lUserID);


        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);

        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_Cleanup();

        public delegate void MSGCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        /*Prototipos de callbacks*/
        [DllImport(rutaLibreriaSDK)]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V50(int iIndex, MSGCallBack fMessageCallBack, IntPtr pUser);



        #endregion}

    }
}