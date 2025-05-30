namespace DeportNetReconocimiento.Hikvision.SDKHikvision
{
    internal class Hik_Evento_Mapper
    {


        public static void AlarmMinorTypeMap(Hik_SDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp = null;
            switch (stLogInfo.dwMinorType)
            {
                //alarm
                case Hik_SDK.MINOR_ALARMIN_SHORT_CIRCUIT:
                    szTemp = string.Format("MINOR_ALARMIN_SHORT_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_ALARMIN_BROKEN_CIRCUIT:
                    szTemp = string.Format("MINOR_ALARMIN_BROKEN_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_ALARMIN_EXCEPTION:
                    szTemp = string.Format("MINOR_ALARMIN_EXCEPTION");
                    break;
                case Hik_SDK.MINOR_ALARMIN_RESUME:
                    szTemp = string.Format("MINOR_ALARMIN_RESUME");
                    break;
                case Hik_SDK.MINOR_HOST_DESMANTLE_ALARM:
                    szTemp = string.Format("MINOR_HOST_DESMANTLE_ALARM");
                    break;
                case Hik_SDK.MINOR_HOST_DESMANTLE_RESUME:
                    szTemp = string.Format("MINOR_HOST_DESMANTLE_RESUME");
                    break;
                case Hik_SDK.MINOR_CARD_READER_DESMANTLE_ALARM:
                    szTemp = string.Format("MINOR_CARD_READER_DESMANTLE_ALARM");
                    break;
                case Hik_SDK.MINOR_CARD_READER_DESMANTLE_RESUME:
                    szTemp = string.Format("MINOR_CARD_READER_DESMANTLE_RESUME");
                    break;
                case Hik_SDK.MINOR_CASE_SENSOR_ALARM:
                    szTemp = string.Format("MINOR_CASE_SENSOR_ALARM");
                    break;
                case Hik_SDK.MINOR_CASE_SENSOR_RESUME:
                    szTemp = string.Format("MINOR_CASE_SENSOR_RESUME");
                    break;
                case Hik_SDK.MINOR_STRESS_ALARM:
                    szTemp = string.Format("MINOR_STRESS_ALARM");
                    break;
                case Hik_SDK.MINOR_OFFLINE_ECENT_NEARLY_FULL:
                    szTemp = string.Format("MINOR_OFFLINE_ECENT_NEARLY_FULL");
                    break;
                case Hik_SDK.MINOR_CARD_MAX_AUTHENTICATE_FAIL:
                    szTemp = string.Format("MINOR_CARD_MAX_AUTHENTICATE_FAIL");
                    break;
                case Hik_SDK.MINOR_SD_CARD_FULL:
                    szTemp = string.Format("MINOR_SD_CARD_FULL");
                    break;
                case Hik_SDK.MINOR_LINKAGE_CAPTURE_PIC:
                    szTemp = string.Format("MINOR_LINKAGE_CAPTURE_PIC");
                    break;
                case Hik_SDK.MINOR_SECURITY_MODULE_DESMANTLE_ALARM:
                    szTemp = string.Format("MINOR_SECURITY_MODULE_DESMANTLE_ALARM");
                    break;
                case Hik_SDK.MINOR_SECURITY_MODULE_DESMANTLE_RESUME:
                    szTemp = string.Format("MINOR_SECURITY_MODULE_DESMANTLE_RESUME");
                    break;
                case Hik_SDK.MINOR_POS_START_ALARM:
                    szTemp = string.Format("MINOR_POS_START_ALARM");
                    break;
                case Hik_SDK.MINOR_POS_END_ALARM:
                    szTemp = string.Format("MINOR_POS_END_ALARM");
                    break;
                case Hik_SDK.MINOR_FACE_IMAGE_QUALITY_LOW:
                    szTemp = string.Format("MINOR_FACE_IMAGE_QUALITY_LOW");
                    break;
                case Hik_SDK.MINOR_FINGE_RPRINT_QUALITY_LOW:
                    szTemp = string.Format("MINOR_FINGE_RPRINT_QUALITY_LOW");
                    break;
                case Hik_SDK.MINOR_FIRE_IMPORT_SHORT_CIRCUIT:
                    szTemp = string.Format("MINOR_FIRE_IMPORT_SHORT_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_FIRE_IMPORT_BROKEN_CIRCUIT:
                    szTemp = string.Format("MINOR_FIRE_IMPORT_BROKEN_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_FIRE_IMPORT_RESUME:
                    szTemp = string.Format("MINOR_FIRE_IMPORT_RESUME");
                    break;
                case Hik_SDK.MINOR_FIRE_BUTTON_TRIGGER:
                    szTemp = string.Format("MINOR_FIRE_BUTTON_TRIGGER");
                    break;
                case Hik_SDK.MINOR_FIRE_BUTTON_RESUME:
                    szTemp = string.Format("MINOR_FIRE_BUTTON_RESUME");
                    break;
                case Hik_SDK.MINOR_MAINTENANCE_BUTTON_TRIGGER:
                    szTemp = string.Format("MINOR_MAINTENANCE_BUTTON_TRIGGER");
                    break;
                case Hik_SDK.MINOR_MAINTENANCE_BUTTON_RESUME:
                    szTemp = string.Format("MINOR_MAINTENANCE_BUTTON_RESUME");
                    break;
                case Hik_SDK.MINOR_EMERGENCY_BUTTON_TRIGGER:
                    szTemp = string.Format("MINOR_EMERGENCY_BUTTON_TRIGGER");
                    break;
                case Hik_SDK.MINOR_EMERGENCY_BUTTON_RESUME:
                    szTemp = string.Format("MINOR_EMERGENCY_BUTTON_RESUME");
                    break;
                case Hik_SDK.MINOR_DISTRACT_CONTROLLER_ALARM:
                    szTemp = string.Format("MINOR_DISTRACT_CONTROLLER_ALARM");
                    break;
                case Hik_SDK.MINOR_DISTRACT_CONTROLLER_RESUME:
                    szTemp = string.Format("MINOR_DISTRACT_CONTROLLER_RESUME");
                    break;
                default:
                    szTemp = string.Format("0x" + stLogInfo.dwMinorType);
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void OperationMinorTypeMap(Hik_SDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp = null;
            char[] csParaType = new char[256];
            switch (stLogInfo.dwMinorType)
            {
                //operation
                case Hik_SDK.MINOR_LOCAL_UPGRADE:
                    szTemp = string.Format("MINOR_LOCAL_UPGRADE");
                    break;
                case Hik_SDK.MINOR_REMOTE_LOGIN:
                    szTemp = string.Format("REMOTE_LOGIN");
                    break;
                case Hik_SDK.MINOR_REMOTE_LOGOUT:
                    szTemp = string.Format("REMOTE_LOGOUT");
                    break;
                case Hik_SDK.MINOR_REMOTE_ARM:
                    szTemp = string.Format("REMOTE_ARM");
                    break;
                case Hik_SDK.MINOR_REMOTE_DISARM:
                    szTemp = string.Format("REMOTE_DISARM");
                    break;
                case Hik_SDK.MINOR_REMOTE_REBOOT:
                    szTemp = string.Format("REMOTE_REBOOT");
                    break;
                case Hik_SDK.MINOR_REMOTE_UPGRADE:
                    szTemp = string.Format("REMOTE_UPGRADE");
                    break;
                case Hik_SDK.MINOR_REMOTE_CFGFILE_OUTPUT:
                    szTemp = string.Format("REMOTE_CFGFILE_OUTPUT");
                    break;
                case Hik_SDK.MINOR_REMOTE_CFGFILE_INTPUT:
                    szTemp = string.Format("REMOTE_CFGFILE_INTPUT");
                    break;
                case Hik_SDK.MINOR_REMOTE_ALARMOUT_OPEN_MAN:
                    szTemp = string.Format("MINOR_REMOTE_ALARMOUT_OPEN_MAN");
                    break;
                case Hik_SDK.MINOR_REMOTE_ALARMOUT_CLOSE_MAN:
                    szTemp = string.Format("MINOR_REMOTE_ALARMOUT_CLOSE_MAN");
                    break;
                case Hik_SDK.MINOR_REMOTE_OPEN_DOOR:
                    szTemp = string.Format("MINOR_REMOTE_OPEN_DOOR");
                    break;
                case Hik_SDK.MINOR_REMOTE_CLOSE_DOOR:
                    szTemp = string.Format("MINOR_REMOTE_CLOSE_DOOR");
                    break;
                case Hik_SDK.MINOR_REMOTE_ALWAYS_OPEN:
                    szTemp = string.Format("MINOR_REMOTE_ALWAYS_OPEN");
                    break;
                case Hik_SDK.MINOR_REMOTE_ALWAYS_CLOSE:
                    szTemp = string.Format("MINOR_REMOTE_ALWAYS_CLOSE");
                    break;
                case Hik_SDK.MINOR_REMOTE_CHECK_TIME:
                    szTemp = string.Format("MINOR_REMOTE_CHECK_TIME");
                    break;
                case Hik_SDK.MINOR_NTP_CHECK_TIME:
                    szTemp = string.Format("MINOR_NTP_CHECK_TIME");
                    break;
                case Hik_SDK.MINOR_REMOTE_CLEAR_CARD:
                    szTemp = string.Format("MINOR_REMOTE_CLEAR_CARD"); ;
                    break;
                case Hik_SDK.MINOR_REMOTE_RESTORE_CFG:
                    szTemp = string.Format("MINOR_REMOTE_RESTORE_CFG");
                    break;
                case Hik_SDK.MINOR_ALARMIN_ARM:
                    szTemp = string.Format("MINOR_ALARMIN_ARM");
                    break;
                case Hik_SDK.MINOR_ALARMIN_DISARM:
                    szTemp = string.Format("MINOR_ALARMIN_DISARM");
                    break;
                case Hik_SDK.MINOR_LOCAL_RESTORE_CFG:
                    szTemp = string.Format("MINOR_LOCAL_RESTORE_CFG");
                    break;
                case Hik_SDK.MINOR_MOD_NET_REPORT_CFG:
                    szTemp = string.Format("MINOR_MOD_NET_REPORT_CFG");
                    break;
                case Hik_SDK.MINOR_MOD_GPRS_REPORT_PARAM:
                    szTemp = string.Format("MINOR_MOD_GPRS_REPORT_PARAM");
                    break;
                case Hik_SDK.MINOR_MOD_REPORT_GROUP_PARAM:
                    szTemp = string.Format("MINOR_MOD_REPORT_GROUP_PARAM");
                    break;
                case Hik_SDK.MINOR_UNLOCK_PASSWORD_OPEN_DOOR:
                    szTemp = string.Format("MINOR_UNLOCK_PASSWORD_OPEN_DOOR");
                    break;
                case Hik_SDK.MINOR_REMOTE_CAPTURE_PIC:
                    szTemp = string.Format("MINOR_REMOTE_CAPTURE_PIC"); ;
                    break;
                case Hik_SDK.MINOR_AUTO_RENUMBER:
                    szTemp = string.Format("MINOR_AUTO_RENUMBER");
                    break;
                case Hik_SDK.MINOR_AUTO_COMPLEMENT_NUMBER:
                    szTemp = string.Format("MINOR_AUTO_COMPLEMENT_NUMBER");
                    break;
                case Hik_SDK.MINOR_NORMAL_CFGFILE_INPUT:
                    szTemp = string.Format("MINOR_NORMAL_CFGFILE_INPUT");
                    break;
                case Hik_SDK.MINOR_NORMAL_CFGFILE_OUTTPUT:
                    szTemp = string.Format("MINOR_NORMAL_CFGFILE_OUTTPUT");
                    break;
                case Hik_SDK.MINOR_CARD_RIGHT_INPUT:
                    szTemp = string.Format("MINOR_CARD_RIGHT_INPUT");
                    break;
                case Hik_SDK.MINOR_CARD_RIGHT_OUTTPUT:
                    szTemp = string.Format("MINOR_CARD_RIGHT_OUTTPUT");
                    break;
                case Hik_SDK.MINOR_LOCAL_USB_UPGRADE:
                    szTemp = string.Format("MINOR_LOCAL_USB_UPGRADE");
                    break;
                case Hik_SDK.MINOR_REMOTE_VISITOR_CALL_LADDER:
                    szTemp = string.Format("MINOR_REMOTE_VISITOR_CALL_LADDER"); ;
                    break;
                case Hik_SDK.MINOR_REMOTE_HOUSEHOLD_CALL_LADDER:
                    szTemp = string.Format("MINOR_REMOTE_HOUSEHOLD_CALL_LADDER"); ;
                    break;
                default:
                    szTemp = string.Format("0x" + stLogInfo.dwMinorType);
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void ExceptionMinorTypeMap(Hik_SDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp = null;
            switch (stLogInfo.dwMinorType)
            {
                //exception
                case Hik_SDK.MINOR_NET_BROKEN:
                    szTemp = string.Format("MINOR_NET_BROKEN");
                    break;
                case Hik_SDK.MINOR_RS485_DEVICE_ABNORMAL:
                    szTemp = string.Format("MINOR_RS485_DEVICE_ABNORMAL");
                    break;
                case Hik_SDK.MINOR_RS485_DEVICE_REVERT:
                    szTemp = string.Format("MINOR_RS485_DEVICE_REVERT");
                    break;
                case Hik_SDK.MINOR_DEV_POWER_ON:
                    szTemp = string.Format("MINOR_DEV_POWER_ON");
                    break;
                case Hik_SDK.MINOR_DEV_POWER_OFF:
                    szTemp = string.Format("MINOR_DEV_POWER_OFF");
                    break;
                case Hik_SDK.MINOR_WATCH_DOG_RESET:
                    szTemp = string.Format("MINOR_WATCH_DOG_RESET");
                    break;
                case Hik_SDK.MINOR_LOW_BATTERY:
                    szTemp = string.Format("MINOR_LOW_BATTERY");
                    break;
                case Hik_SDK.MINOR_BATTERY_RESUME:
                    szTemp = string.Format("MINOR_BATTERY_RESUME");
                    break;
                case Hik_SDK.MINOR_AC_OFF:
                    szTemp = string.Format("MINOR_AC_OFF");
                    break;
                case Hik_SDK.MINOR_AC_RESUME:
                    szTemp = string.Format("MINOR_AC_RESUME");
                    break;
                case Hik_SDK.MINOR_NET_RESUME:
                    szTemp = string.Format("MINOR_NET_RESUME");
                    break;
                case Hik_SDK.MINOR_FLASH_ABNORMAL:
                    szTemp = string.Format("MINOR_FLASH_ABNORMAL");
                    break;
                case Hik_SDK.MINOR_CARD_READER_OFFLINE:
                    szTemp = string.Format("MINOR_CARD_READER_OFFLINE");
                    break;
                case Hik_SDK.MINOR_CARD_READER_RESUME:
                    szTemp = string.Format("MINOR_CAED_READER_RESUME");
                    break;
                case Hik_SDK.MINOR_INDICATOR_LIGHT_OFF:
                    szTemp = string.Format("MINOR_INDICATOR_LIGHT_OFF");
                    break;
                case Hik_SDK.MINOR_INDICATOR_LIGHT_RESUME:
                    szTemp = string.Format("MINOR_INDICATOR_LIGHT_RESUME");
                    break;
                case Hik_SDK.MINOR_CHANNEL_CONTROLLER_OFF:
                    szTemp = string.Format("MINOR_CHANNEL_CONTROLLER_OFF");
                    break;
                case Hik_SDK.MINOR_CHANNEL_CONTROLLER_RESUME:
                    szTemp = string.Format("MINOR_CHANNEL_CONTROLLER_RESUME");
                    break;
                case Hik_SDK.MINOR_SECURITY_MODULE_OFF:
                    szTemp = string.Format("MINOR_SECURITY_MODULE_OFF");
                    break;
                case Hik_SDK.MINOR_SECURITY_MODULE_RESUME:
                    szTemp = string.Format("MINOR_SECURITY_MODULE_RESUME");
                    break;
                case Hik_SDK.MINOR_BATTERY_ELECTRIC_LOW:
                    szTemp = string.Format("MINOR_BATTERY_ELECTRIC_LOW");
                    break;
                case Hik_SDK.MINOR_BATTERY_ELECTRIC_RESUME:
                    szTemp = string.Format("MINOR_BATTERY_ELECTRIC_RESUME");
                    break;
                case Hik_SDK.MINOR_LOCAL_CONTROL_NET_BROKEN:
                    szTemp = string.Format("MINOR_LOCAL_CONTROL_NET_BROKEN");
                    break;
                case Hik_SDK.MINOR_LOCAL_CONTROL_NET_RSUME:
                    szTemp = string.Format("MINOR_LOCAL_CONTROL_NET_RSUME");
                    break;
                case Hik_SDK.MINOR_MASTER_RS485_LOOPNODE_BROKEN:
                    szTemp = string.Format("MINOR_MASTER_RS485_LOOPNODE_BROKEN");
                    break;
                case Hik_SDK.MINOR_MASTER_RS485_LOOPNODE_RESUME:
                    szTemp = string.Format("MINOR_MASTER_RS485_LOOPNODE_RESUME");
                    break;
                case Hik_SDK.MINOR_LOCAL_CONTROL_OFFLINE:
                    szTemp = string.Format("MINOR_LOCAL_CONTROL_OFFLINE");
                    break;
                case Hik_SDK.MINOR_LOCAL_CONTROL_RESUME:
                    szTemp = string.Format("MINOR_LOCAL_CONTROL_RESUME");
                    break;
                case Hik_SDK.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN:
                    szTemp = string.Format("MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_BROKEN");
                    break;
                case Hik_SDK.MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME:
                    szTemp = string.Format("MINOR_LOCAL_DOWNSIDE_RS485_LOOPNODE_RESUME");
                    break;
                case Hik_SDK.MINOR_DISTRACT_CONTROLLER_ONLINE:
                    szTemp = string.Format("MINOR_DISTRACT_CONTROLLER_ONLINE");
                    break;
                case Hik_SDK.MINOR_DISTRACT_CONTROLLER_OFFLINE:
                    szTemp = string.Format("MINOR_DISTRACT_CONTROLLER_OFFLINE");
                    break;
                case Hik_SDK.MINOR_ID_CARD_READER_NOT_CONNECT:
                    szTemp = string.Format("MINOR_ID_CARD_READER_NOT_CONNECT");
                    break;
                case Hik_SDK.MINOR_ID_CARD_READER_RESUME:
                    szTemp = string.Format("MINOR_ID_CARD_READER_RESUME");
                    break;
                case Hik_SDK.MINOR_FINGER_PRINT_MODULE_NOT_CONNECT:
                    szTemp = string.Format("MINOR_FINGER_PRINT_MODULE_NOT_CONNECT");
                    break;
                case Hik_SDK.MINOR_FINGER_PRINT_MODULE_RESUME:
                    szTemp = string.Format("MINOR_FINGER_PRINT_MODULE_RESUME");
                    break;
                case Hik_SDK.MINOR_CAMERA_NOT_CONNECT:
                    szTemp = string.Format("MINOR_CAMERA_NOT_CONNECT");
                    break;
                case Hik_SDK.MINOR_CAMERA_RESUME:
                    szTemp = string.Format("MINOR_CAMERA_RESUME");
                    break;
                case Hik_SDK.MINOR_COM_NOT_CONNECT:
                    szTemp = string.Format("MINOR_COM_NOT_CONNECT");
                    break;
                case Hik_SDK.MINOR_COM_RESUME:
                    szTemp = string.Format("MINOR_COM_RESUME");
                    break;
                case Hik_SDK.MINOR_DEVICE_NOT_AUTHORIZE:
                    szTemp = string.Format("MINOR_DEVICE_NOT_AUTHORIZE");
                    break;
                case Hik_SDK.MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE:
                    szTemp = string.Format("MINOR_PEOPLE_AND_ID_CARD_DEVICE_ONLINE");
                    break;
                case Hik_SDK.MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE:
                    szTemp = string.Format("MINOR_PEOPLE_AND_ID_CARD_DEVICE_OFFLINE");
                    break;
                case Hik_SDK.MINOR_LOCAL_LOGIN_LOCK:
                    szTemp = string.Format("MINOR_LOCAL_LOGIN_LOCK");
                    break;
                case Hik_SDK.MINOR_LOCAL_LOGIN_UNLOCK:
                    szTemp = string.Format("MINOR_LOCAL_LOGIN_UNLOCK");
                    break;
                default:
                    szTemp = string.Format("0x" + stLogInfo.dwMinorType);
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }

        public static void EventMinorTypeMap(Hik_SDK.NET_DVR_LOG_V30 stLogInfo, char[] csTmp)
        {
            string szTemp = null;
            switch (stLogInfo.dwMinorType)
            {
                case Hik_SDK.MINOR_LEGAL_CARD_PASS:
                    szTemp = string.Format("MINOR_LEGAL_CARD_PASS");
                    break;
                case Hik_SDK.MINOR_CARD_AND_PSW_PASS:
                    szTemp = string.Format("MINOR_CARD_AND_PSW_PASS");
                    break;
                case Hik_SDK.MINOR_CARD_AND_PSW_FAIL:
                    szTemp = string.Format("MINOR_CARD_AND_PSW_FAIL");
                    break;
                case Hik_SDK.MINOR_CARD_AND_PSW_TIMEOUT:
                    szTemp = string.Format("MINOR_CARD_AND_PSW_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_CARD_AND_PSW_OVER_TIME:
                    szTemp = string.Format("MINOR_CARD_AND_PSW_OVER_TIME");
                    break;
                case Hik_SDK.MINOR_CARD_NO_RIGHT:
                    szTemp = string.Format("MINOR_CARD_NO_RIGHT");
                    break;
                case Hik_SDK.MINOR_CARD_INVALID_PERIOD:
                    szTemp = string.Format("MINOR_CARD_INVALID_PERIOD");
                    break;
                case Hik_SDK.MINOR_CARD_OUT_OF_DATE:
                    szTemp = string.Format("MINOR_CARD_OUT_OF_DATE");
                    break;
                case Hik_SDK.MINOR_INVALID_CARD:
                    szTemp = string.Format("MINOR_INVALID_CARD");
                    break;
                case Hik_SDK.MINOR_ANTI_SNEAK_FAIL:
                    szTemp = string.Format("MINOR_ANTI_SNEAK_FAIL");
                    break;
                case Hik_SDK.MINOR_INTERLOCK_DOOR_NOT_CLOSE:
                    szTemp = string.Format("MINOR_INTERLOCK_DOOR_NOT_CLOSE");
                    break;
                case Hik_SDK.MINOR_NOT_BELONG_MULTI_GROUP:
                    szTemp = string.Format("MINOR_NOT_BELONG_MULTI_GROUP");
                    break;
                case Hik_SDK.MINOR_INVALID_MULTI_VERIFY_PERIOD:
                    szTemp = string.Format("MINOR_INVALID_MULTI_VERIFY_PERIOD");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_SUCCESS:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_SUCCESS");
                    break;
                case Hik_SDK.MINOR_LEADER_CARD_OPEN_BEGIN:
                    szTemp = string.Format("MINOR_LEADER_CARD_OPEN_BEGIN");
                    break;
                case Hik_SDK.MINOR_LEADER_CARD_OPEN_END:
                    szTemp = string.Format("MINOR_LEADER_CARD_OPEN_END");
                    break;
                case Hik_SDK.MINOR_ALWAYS_OPEN_BEGIN:
                    szTemp = string.Format("MINOR_ALWAYS_OPEN_BEGIN");
                    break;
                case Hik_SDK.MINOR_ALWAYS_OPEN_END:
                    szTemp = string.Format("MINOR_ALWAYS_OPEN_END");
                    break;
                case Hik_SDK.MINOR_LOCK_OPEN:
                    szTemp = string.Format("MINOR_LOCK_OPEN");
                    break;
                case Hik_SDK.MINOR_LOCK_CLOSE:
                    szTemp = string.Format("MINOR_LOCK_CLOSE");
                    break;
                case Hik_SDK.MINOR_DOOR_BUTTON_PRESS:
                    szTemp = string.Format("MINOR_DOOR_BUTTON_PRESS");
                    break;
                case Hik_SDK.MINOR_DOOR_BUTTON_RELEASE:
                    szTemp = string.Format("MINOR_DOOR_BUTTON_RELEASE");
                    break;
                case Hik_SDK.MINOR_DOOR_OPEN_NORMAL:
                    szTemp = string.Format("MINOR_DOOR_OPEN_NORMAL");
                    break;
                case Hik_SDK.MINOR_DOOR_CLOSE_NORMAL:
                    szTemp = string.Format("MINOR_DOOR_CLOSE_NORMAL");
                    break;
                case Hik_SDK.MINOR_DOOR_OPEN_ABNORMAL:
                    szTemp = string.Format("MINOR_DOOR_OPEN_ABNORMAL");
                    break;
                case Hik_SDK.MINOR_DOOR_OPEN_TIMEOUT:
                    szTemp = string.Format("MINOR_DOOR_OPEN_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_ALARMOUT_ON:
                    szTemp = string.Format("MINOR_ALARMOUT_ON");
                    break;
                case Hik_SDK.MINOR_ALARMOUT_OFF:
                    szTemp = string.Format("MINOR_ALARMOUT_OFF");
                    break;
                case Hik_SDK.MINOR_ALWAYS_CLOSE_BEGIN:
                    szTemp = string.Format("MINOR_ALWAYS_CLOSE_BEGIN");
                    break;
                case Hik_SDK.MINOR_ALWAYS_CLOSE_END:
                    szTemp = string.Format("MINOR_ALWAYS_CLOSE_END");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_REPEAT_VERIFY:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_REPEAT_VERIFY");
                    break;
                case Hik_SDK.MINOR_MULTI_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_MULTI_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_DOORBELL_RINGING:
                    szTemp = string.Format("MINOR_DOORBELL_RINGING");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_COMPARE_PASS:
                    szTemp = string.Format("MINOR_FINGERPRINT_COMPARE_PASS");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_COMPARE_FAIL:
                    szTemp = string.Format("MINOR_FINGERPRINT_COMPARE_FAIL");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_VERIFY_PASS:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_PASSWD_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FINGERPRINT_PASSWD_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FINGERPRINT_INEXISTENCE:
                    szTemp = string.Format("MINOR_FINGERPRINT_INEXISTENCE");
                    break;
                case Hik_SDK.MINOR_CARD_PLATFORM_VERIFY:
                    szTemp = string.Format("MINOR_CARD_PLATFORM_VERIFY");
                    break;
                case Hik_SDK.MINOR_CALL_CENTER:
                    szTemp = string.Format("MINOR_CALL_CENTER");
                    break;
                case Hik_SDK.MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN:
                    szTemp = string.Format("MINOR_FIRE_RELAY_TURN_ON_DOOR_ALWAYS_OPEN");
                    break;
                case Hik_SDK.MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL:
                    szTemp = string.Format("MINOR_FIRE_RELAY_RECOVER_DOOR_RECOVER_NORMAL");
                    break;
                case Hik_SDK.MINOR_FACE_AND_FP_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_AND_FP_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_AND_FP_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_AND_FP_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FACE_AND_FP_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FACE_AND_FP_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_AND_PW_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_AND_PW_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FACE_AND_PW_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_AND_CARD_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_AND_CARD_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_AND_CARD_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_AND_CARD_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FACE_AND_CARD_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FACE_AND_CARD_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_AND_PW_AND_FP_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_AND_PW_AND_FP_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FACE_AND_PW_AND_FP_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_CARD_AND_FP_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_CARD_AND_FP_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_CARD_AND_FP_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_CARD_AND_FP_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_FACE_CARD_AND_FP_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FP_AND_PW_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_VERIFY_PASS:
                    szTemp = string.Format("MINOR_FACE_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_FACE_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_FACE_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FACE_VERIFY_PASS");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FACE_VERIFY_FAIL");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_FACE_VERIFY_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FACE_RECOGNIZE_FAIL:
                    szTemp = string.Format("MINOR_FACE_RECOGNIZE_FAIL");
                    break;
                case Hik_SDK.MINOR_FIRSTCARD_AUTHORIZE_BEGIN:
                    szTemp = string.Format("MINOR_FIRSTCARD_AUTHORIZE_BEGIN");
                    break;
                case Hik_SDK.MINOR_FIRSTCARD_AUTHORIZE_END:
                    szTemp = string.Format("MINOR_FIRSTCARD_AUTHORIZE_END");
                    break;
                case Hik_SDK.MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT:
                    szTemp = string.Format("MINOR_DOORLOCK_INPUT_SHORT_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT:
                    szTemp = string.Format("MINOR_DOORLOCK_INPUT_BROKEN_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_DOORLOCK_INPUT_EXCEPTION:
                    szTemp = string.Format("MINOR_DOORLOCK_INPUT_EXCEPTION");
                    break;
                case Hik_SDK.MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT:
                    szTemp = string.Format("MINOR_DOORCONTACT_INPUT_SHORT_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT:
                    szTemp = string.Format("MINOR_DOORCONTACT_INPUT_BROKEN_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_DOORCONTACT_INPUT_EXCEPTION:
                    szTemp = string.Format("MINOR_DOORCONTACT_INPUT_EXCEPTION");
                    break;
                case Hik_SDK.MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT:
                    szTemp = string.Format("MINOR_OPENBUTTON_INPUT_SHORT_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT:
                    szTemp = string.Format("MINOR_OPENBUTTON_INPUT_BROKEN_CIRCUIT");
                    break;
                case Hik_SDK.MINOR_OPENBUTTON_INPUT_EXCEPTION:
                    szTemp = string.Format("MINOR_OPENBUTTON_INPUT_EXCEPTION");
                    break;
                case Hik_SDK.MINOR_DOORLOCK_OPEN_EXCEPTION:
                    szTemp = string.Format("MINOR_DOORLOCK_OPEN_EXCEPTION");
                    break;
                case Hik_SDK.MINOR_DOORLOCK_OPEN_TIMEOUT:
                    szTemp = string.Format("MINOR_DOORLOCK_OPEN_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE:
                    szTemp = string.Format("MINOR_FIRSTCARD_OPEN_WITHOUT_AUTHORIZE");
                    break;
                case Hik_SDK.MINOR_CALL_LADDER_RELAY_BREAK:
                    szTemp = string.Format("MINOR_CALL_LADDER_RELAY_BREAK");
                    break;
                case Hik_SDK.MINOR_CALL_LADDER_RELAY_CLOSE:
                    szTemp = string.Format("MINOR_CALL_LADDER_RELAY_CLOSE");
                    break;
                case Hik_SDK.MINOR_AUTO_KEY_RELAY_BREAK:
                    szTemp = string.Format("MINOR_AUTO_KEY_RELAY_BREAK");
                    break;
                case Hik_SDK.MINOR_AUTO_KEY_RELAY_CLOSE:
                    szTemp = string.Format("MINOR_AUTO_KEY_RELAY_CLOSE");
                    break;
                case Hik_SDK.MINOR_KEY_CONTROL_RELAY_BREAK:
                    szTemp = string.Format("MINOR_KEY_CONTROL_RELAY_BREAK");
                    break;
                case Hik_SDK.MINOR_KEY_CONTROL_RELAY_CLOSE:
                    szTemp = string.Format("MINOR_KEY_CONTROL_RELAY_CLOSE");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_PW_PASS:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_PW_PASS");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_PW_FAIL:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_PW_FAIL");
                    break;
                case Hik_SDK.MINOR_EMPLOYEENO_AND_PW_TIMEOUT:
                    szTemp = string.Format("MINOR_EMPLOYEENO_AND_PW_TIMEOUT");
                    break;
                case Hik_SDK.MINOR_HUMAN_DETECT_FAIL:
                    szTemp = string.Format("MINOR_HUMAN_DETECT_FAIL");
                    break;
                case Hik_SDK.MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS:
                    szTemp = string.Format("MINOR_PEOPLE_AND_ID_CARD_COMPARE_PASS");
                    break;
                case Hik_SDK.MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL:
                    szTemp = string.Format("MINOR_PEOPLE_AND_ID_CARD_COMPARE_FAIL");
                    break;
                case Hik_SDK.MINOR_CERTIFICATE_BLOCK_LIST:
                    szTemp = string.Format("MINOR_CERTIFICATE_BLOCK_LIST");
                    break;
                case Hik_SDK.MINOR_LEGAL_MESSAGE:
                    szTemp = string.Format("MINOR_LEGAL_MESSAGE");
                    break;
                case Hik_SDK.MINOR_ILLEGAL_MESSAGE:
                    szTemp = string.Format("MINOR_ILLEGAL_MESSAGE");
                    break;
                case Hik_SDK.MINOR_MAC_DETECT:
                    szTemp = string.Format("MINOR_MAC_DETECT");
                    break;
                default:
                    szTemp = string.Format("Main Event unknown:" + "0x" + "stLogInfo.dwMinorType");
                    break;
            }
            szTemp.CopyTo(0, csTmp, 0, szTemp.Length);
            return;
        }




    }
}
