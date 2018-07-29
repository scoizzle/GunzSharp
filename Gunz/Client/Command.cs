using System;
using System.Linq;
using System.Text;

namespace Gunz {
    public sealed class Command {
        public const ushort MATCH_LOGIN = 0x3E9;
        public const ushort MATCH_REQUESTACCOUNTCHARLIST = 0x6A5;
        public const ushort MATCH_REQUESTCREATECHAR = 0x6AF;
        public const ushort MATCH_REQUESTDELETECHAR = 0x6B1;
        public const ushort MATCH_REQUESTSELECTCHAR = 0x6A7;
        public const ushort MATCH_RESPONSE_RESULT = 0x193;
        public const ushort MATCH_RESPONSEACCOUNTCHARLIST = 0x6A6;
        public const ushort MATCH_RESPONSECREATECHAR = 0x6B0;
        public const ushort MATCH_RESPONSEDELETECHAR = 0x6B2;
        public const ushort MATCH_RESPONSESELECTCHAR = 0x6A8;
        public const ushort MATCH_RESPONSELOGIN = 0x3EA;
        public const ushort MATCH_WHISPER = 0x641;
        public const ushort MATCHSERVER_REQUESTRECOMMENDEDCHANNEL = 0x4B1;
        public const ushort MATCHSERVER_RESPONSERECOMMENDEDCHANNEL = 0x4B2;
        public const ushort NET_PING = 0x142;
        public const ushort NET_PONG = 0x143;
        public const ushort RESPONSE_ACCOUNT_IN_USE = 10001;
        public const ushort RESPONSE_AUTHENTICATION_FAILED = 10005;
        public const ushort RESPONSE_BANNED = 10004;
        public const ushort RESPONSE_CHANNEL_CANNOT_ENTER = 30020;
        public const ushort RESPONSE_CHANNEL_HAS_LEVEL_21_CHARACTER = 30023;
        public const ushort RESPONSE_CHANNEL_INVALID_LEVEL = 30022;
        public const ushort RESPONSE_CHANNEL_IS_FULL = 30021;
        public const ushort RESPONSE_CHARACTER_DOESNT_EXIST = 10110;
        public const ushort RESPONSE_CHARACTER_IN_CLAN_OR_CASH_ITEMS = 10111;
        public const ushort RESPONSE_INVALID_PASSWORD = 10000;
        public const ushort RESPONSE_INVALID_VERSION = 10002;
        public const ushort RESPONSE_NAME_EMPTY = 10104;
        public const ushort RESPONSE_NAME_IN_USE = 10100;
        public const ushort RESPONSE_NAME_INVALID_CHARACTERS = 10101;
        public const ushort RESPONSE_NAME_TOO_LONG = 10103;
        public const ushort RESPONSE_NAME_TOO_SHORT = 10102;
        public const ushort RESPONSE_OK = 0;
        public const ushort RESPONSE_SERVER_IS_FULL = 10003;
    }
}