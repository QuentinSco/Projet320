
#pragma once

#include "StdAfx.h"

#include <vector>

#include "Types.h"

using namespace std;

class IEventDescriptor
{
protected:
    HardwareSource Source;

public:
    DeviceType Device;
    int ChipAddress;
    std::string Name;
    int Index;

    virtual ~IEventDescriptor()
    {
    }
};

class BaseEvent
{
    friend class Serializer;
protected:
    PacketType m_packetType;

protected:
    BaseEvent(PacketType packetType) : m_packetType(packetType)
    {
    }

public:
    virtual ~BaseEvent()
    {
    }

    PacketType GetPacketType()
    {
        return this->m_packetType;
    }
};

class IOEvent : public BaseEvent
{
    friend class Serializer;

protected:
    IOEvent() : BaseEvent(PacketType::HardwareEvent)
    {
    }

public:
    int ChipAddress;
    DeviceType Device;
    int Index;
    HardwareSource Source;
    int Event;
    Group Group;
    int Value;
};

class HardwareConnectionStateEvent : public BaseEvent
{
    friend class Serializer;

protected:
    HardwareConnectionStateEvent() : BaseEvent(PacketType::ConnectionStateEvent)
    {
    }

public:
    DeviceType Device;
    bool Connected;
};

typedef void (*IOCallback)(IOEvent*, void*);
typedef void (*DeviceChangeCallback)(HardwareConnectionStateEvent*, void*);

class EventClient
{
public:
    void Connect();
    void Disconnect();
    void RegisterEvents(vector<IEventDescriptor*>&, bool);
    void UnregisterEvents(void*);

    void SetOutputs(void*, bool);
    void SetDisplayText(void*, string);
    void SetLCDText(void*, string);
    void SetServoPosition(void*, int);
    void SetStepperPosition(void*, int, int);

    static EventClient* Create(string endpoint, int port, IOCallback eventCallback, DeviceChangeCallback deviceChangeCallback, void* pState);

protected:
    EventClient()
    {
    }

    virtual ~EventClient()
    {
    }
};

class Switches
{
public:
    class GLARE
    {
    public:
        class WINGS
        {
        public:
            static IEventDescriptor* AUTOLANDCS;
            static IEventDescriptor* CHRONOCS;
            static IEventDescriptor* MASTERCAUTIONCS;
            static IEventDescriptor* MASTERWARNINGCS;
            static IEventDescriptor* SIDESTICKPRIORITYCS;
            static IEventDescriptor* AUTOLANDFO;
            static IEventDescriptor* CHRONOFO;
            static IEventDescriptor* MASTERCAUTIONFO;
            static IEventDescriptor* MASTERWARNINGFO;
            static IEventDescriptor* SIDESTICKPRIORITYFO;

            static std::vector<IEventDescriptor*> All;
        };

        class FCU
        {
        public:
            static IEventDescriptor* ATHR;
            static IEventDescriptor* ALTITUDECHANGE100;
            static IEventDescriptor* ALTITUDECHANGE1000;
            static IEventDescriptor* ALTITUDEPULL;
            static IEventDescriptor* ALTITUDEPUSH;
            static IEventDescriptor* AP1;
            static IEventDescriptor* AP2;
            static IEventDescriptor* APPR;
            static IEventDescriptor* EXPED;
            static IEventDescriptor* HDGVSTRKFPA;
            static IEventDescriptor* HEADINGPULL;
            static IEventDescriptor* HEADINGPUSH;
            static IEventDescriptor* LOC;
            static IEventDescriptor* METRICALT;
            static IEventDescriptor* SPDMACH;
            static IEventDescriptor* SPEEDPULL;
            static IEventDescriptor* SPEEDPUSH;
            static IEventDescriptor* VSPULL;
            static IEventDescriptor* VSPUSH;

            static std::vector<IEventDescriptor*> All;
        };

        class EFIS
        {
        public:
            class FO
            {
            public:
                static IEventDescriptor* ARPT;
                static IEventDescriptor* CSTR;
                static IEventDescriptor* FD;
                static IEventDescriptor* QNHMB;
                static IEventDescriptor* ILS;
                static IEventDescriptor* QNHINHG;
                static IEventDescriptor* KOHLSMANPULL;
                static IEventDescriptor* KOHLSMANPUSH;
                static IEventDescriptor* NAV1ADF;
                static IEventDescriptor* NAV1VOR;
                static IEventDescriptor* NAV2ADF;
                static IEventDescriptor* NAV2VOR;
                static IEventDescriptor* NDARCMODE;
                static IEventDescriptor* NDILSMODE;
                static IEventDescriptor* NDNAVMODE;
                static IEventDescriptor* NDPLANMODE;
                static IEventDescriptor* NDVORMODE;
                static IEventDescriptor* NDB;
                static IEventDescriptor* NDRANGE10;
                static IEventDescriptor* NDRANGE160;
                static IEventDescriptor* NDRANGE20;
                static IEventDescriptor* NDRANGE320;
                static IEventDescriptor* NDRANGE40;
                static IEventDescriptor* NDRANGE80;
                static IEventDescriptor* VORD;
                static IEventDescriptor* WPT;

                static std::vector<IEventDescriptor*> All;
            };

            class CS
            {
            public:
                static IEventDescriptor* ARPT;
                static IEventDescriptor* CSTR;
                static IEventDescriptor* FD;
                static IEventDescriptor* QNHMB;
                static IEventDescriptor* ILS;
                static IEventDescriptor* QNHINHG;
                static IEventDescriptor* KOHLSMANPULL;
                static IEventDescriptor* KOHLSMANPUSH;
                static IEventDescriptor* NDARCMODE;
                static IEventDescriptor* NDILSMODE;
                static IEventDescriptor* NDNAVMODE;
                static IEventDescriptor* NDPLANMODE;
                static IEventDescriptor* NDVORMODE;
                static IEventDescriptor* NAV1ADF;
                static IEventDescriptor* NAV1VOR;
                static IEventDescriptor* NAV2ADF;
                static IEventDescriptor* NAV2VOR;
                static IEventDescriptor* NDB;
                static IEventDescriptor* NDRANGE10;
                static IEventDescriptor* NDRANGE160;
                static IEventDescriptor* NDRANGE20;
                static IEventDescriptor* NDRANGE320;
                static IEventDescriptor* NDRANGE40;
                static IEventDescriptor* NDRANGE80;
                static IEventDescriptor* VORD;
                static IEventDescriptor* WPT;

                static std::vector<IEventDescriptor*> All;
            };

        };

    };

    class PEDESTAL
    {
    public:
        class MCDU
        {
        public:
            class FO
            {
            public:
                static IEventDescriptor* PERIOD;
                static IEventDescriptor* PLUS;
                static IEventDescriptor* KEY0;
                static IEventDescriptor* KEY1;
                static IEventDescriptor* KEY2;
                static IEventDescriptor* KEY3;
                static IEventDescriptor* KEY4;
                static IEventDescriptor* KEY5;
                static IEventDescriptor* KEY6;
                static IEventDescriptor* KEY7;
                static IEventDescriptor* KEY8;
                static IEventDescriptor* KEY9;
                static IEventDescriptor* KEYA;
                static IEventDescriptor* AIRPORT;
                static IEventDescriptor* ARROWDOWN;
                static IEventDescriptor* ARROWLEFT;
                static IEventDescriptor* ARROWRIGHT;
                static IEventDescriptor* ARROWUP;
                static IEventDescriptor* ATCCOMM;
                static IEventDescriptor* KEYB;
                static IEventDescriptor* BRT;
                static IEventDescriptor* KEYC;
                static IEventDescriptor* CLR;
                static IEventDescriptor* KEYD;
                static IEventDescriptor* DATA;
                static IEventDescriptor* DIM;
                static IEventDescriptor* DIR;
                static IEventDescriptor* KEYE;
                static IEventDescriptor* KEYF;
                static IEventDescriptor* FPLAN;
                static IEventDescriptor* FUELPRED;
                static IEventDescriptor* KEYG;
                static IEventDescriptor* KEYH;
                static IEventDescriptor* KEYI;
                static IEventDescriptor* INIT;
                static IEventDescriptor* KEYJ;
                static IEventDescriptor* KEYK;
                static IEventDescriptor* KEYL;
                static IEventDescriptor* LSK1;
                static IEventDescriptor* LSK2;
                static IEventDescriptor* LSK3;
                static IEventDescriptor* LSK4;
                static IEventDescriptor* LSK5;
                static IEventDescriptor* LSK6;
                static IEventDescriptor* KEYM;
                static IEventDescriptor* MCDUMENU;
                static IEventDescriptor* KEYN;
                static IEventDescriptor* KEYO;
                static IEventDescriptor* OVFLY;
                static IEventDescriptor* KEYP;
                static IEventDescriptor* PERF;
                static IEventDescriptor* PROG;
                static IEventDescriptor* KEYQ;
                static IEventDescriptor* KEYR;
                static IEventDescriptor* RSK1;
                static IEventDescriptor* RSK2;
                static IEventDescriptor* RSK3;
                static IEventDescriptor* RSK4;
                static IEventDescriptor* RSK5;
                static IEventDescriptor* RSK6;
                static IEventDescriptor* RADNAV;
                static IEventDescriptor* KEYS;
                static IEventDescriptor* SECFPLAN;
                static IEventDescriptor* SLASH;
                static IEventDescriptor* SPACE;
                static IEventDescriptor* KEYT;
                static IEventDescriptor* KEYU;
                static IEventDescriptor* KEYV;
                static IEventDescriptor* KEYW;
                static IEventDescriptor* KEYX;
                static IEventDescriptor* KEYY;
                static IEventDescriptor* KEYZ;
            };

            class CS
            {
            public:
                static IEventDescriptor* PERIOD;
                static IEventDescriptor* PLUS;
                static IEventDescriptor* KEY0;
                static IEventDescriptor* KEY1;
                static IEventDescriptor* KEY2;
                static IEventDescriptor* KEY3;
                static IEventDescriptor* KEY4;
                static IEventDescriptor* KEY5;
                static IEventDescriptor* KEY6;
                static IEventDescriptor* KEY7;
                static IEventDescriptor* KEY8;
                static IEventDescriptor* KEY9;
                static IEventDescriptor* KEYA;
                static IEventDescriptor* AIRPORT;
                static IEventDescriptor* ARROWDOWN;
                static IEventDescriptor* ARROWLEFT;
                static IEventDescriptor* ARROWRIGHT;
                static IEventDescriptor* ARROWUP;
                static IEventDescriptor* ATCCOMM;
                static IEventDescriptor* KEYB;
                static IEventDescriptor* BRT;
                static IEventDescriptor* KEYC;
                static IEventDescriptor* CLR;
                static IEventDescriptor* KEYD;
                static IEventDescriptor* DATA;
                static IEventDescriptor* DIM;
                static IEventDescriptor* DIR;
                static IEventDescriptor* KEYE;
                static IEventDescriptor* KEYF;
                static IEventDescriptor* FPLAN;
                static IEventDescriptor* FUELPRED;
                static IEventDescriptor* KEYG;
                static IEventDescriptor* KEYH;
                static IEventDescriptor* KEYI;
                static IEventDescriptor* INIT;
                static IEventDescriptor* KEYJ;
                static IEventDescriptor* KEYK;
                static IEventDescriptor* KEYL;
                static IEventDescriptor* LSK1;
                static IEventDescriptor* LSK2;
                static IEventDescriptor* LSK3;
                static IEventDescriptor* LSK4;
                static IEventDescriptor* LSK5;
                static IEventDescriptor* LSK6;
                static IEventDescriptor* KEYM;
                static IEventDescriptor* MCDUMENU;
                static IEventDescriptor* KEYN;
                static IEventDescriptor* KEYO;
                static IEventDescriptor* OVFLY;
                static IEventDescriptor* KEYP;
                static IEventDescriptor* PERF;
                static IEventDescriptor* PROG;
                static IEventDescriptor* KEYQ;
                static IEventDescriptor* KEYR;
                static IEventDescriptor* RSK1;
                static IEventDescriptor* RSK2;
                static IEventDescriptor* RSK3;
                static IEventDescriptor* RSK4;
                static IEventDescriptor* RSK5;
                static IEventDescriptor* RSK6;
                static IEventDescriptor* RADNAV;
                static IEventDescriptor* KEYS;
                static IEventDescriptor* SECFPLAN;
                static IEventDescriptor* SLASH;
                static IEventDescriptor* SPACE;
                static IEventDescriptor* KEYT;
                static IEventDescriptor* KEYU;
                static IEventDescriptor* KEYV;
                static IEventDescriptor* KEYW;
                static IEventDescriptor* KEYX;
                static IEventDescriptor* KEYY;
                static IEventDescriptor* KEYZ;
            };

        };

        class TQ
        {
        public:
            static IEventDescriptor* ENGSTARTCRANK;
            static IEventDescriptor* ENG1MASTERSWITCH;
            static IEventDescriptor* ENG2MASTERSWITCH;
            static IEventDescriptor* ENGSTARTIGN;
            static IEventDescriptor* ENGSTARTNORM;
        };

        class RMP2
        {
        public:
            static IEventDescriptor* ADF;
            static IEventDescriptor* AM;
            static IEventDescriptor* BFO;
            static IEventDescriptor* HF1;
            static IEventDescriptor* HF2;
            static IEventDescriptor* ILS;
            static IEventDescriptor* MLS;
            static IEventDescriptor* NAV;
            static IEventDescriptor* OFF;
            static IEventDescriptor* ON;
            static IEventDescriptor* SWAPSTBYACTIVE;
            static IEventDescriptor* VHF1;
            static IEventDescriptor* VHF2;
            static IEventDescriptor* VHF3;
            static IEventDescriptor* VOR;
        };

        class RMP1
        {
        public:
            static IEventDescriptor* ADF;
            static IEventDescriptor* AM;
            static IEventDescriptor* BFO;
            static IEventDescriptor* HF1;
            static IEventDescriptor* HF2;
            static IEventDescriptor* ILS;
            static IEventDescriptor* MLS;
            static IEventDescriptor* NAV;
            static IEventDescriptor* OFF;
            static IEventDescriptor* ON;
            static IEventDescriptor* SWAPSTBYACTIVE;
            static IEventDescriptor* VHF1;
            static IEventDescriptor* VHF2;
            static IEventDescriptor* VHF3;
            static IEventDescriptor* VOR;
        };

        class LOWER
        {
        public:
            static IEventDescriptor* PARKBRKOFF;
            static IEventDescriptor* PARKBRKON;
            static IEventDescriptor* COCKPITDOORLOCK;
            static IEventDescriptor* COCKPITDOORUNLOCK;
            static IEventDescriptor* RUDDERTRIMLEFT;
            static IEventDescriptor* RUDDERTRIMRESET;
            static IEventDescriptor* RUDDERTRIMRIGHT;
            static IEventDescriptor* ARMGROUNDSPOILERS;
        };

        class ECAM
        {
        public:
            static IEventDescriptor* RADARMAP;
            static IEventDescriptor* RADARSYSOFF;
            static IEventDescriptor* RADARSYSON;
            static IEventDescriptor* RADARWX;
            static IEventDescriptor* RADARWXTURB;
            static IEventDescriptor* ALL;
            static IEventDescriptor* APU;
            static IEventDescriptor* BLEED;
            static IEventDescriptor* CLR1;
            static IEventDescriptor* CLR2;
            static IEventDescriptor* COND;
            static IEventDescriptor* DOOR;
            static IEventDescriptor* ELEC;
            static IEventDescriptor* ENG;
            static IEventDescriptor* FCTL;
            static IEventDescriptor* FUEL;
            static IEventDescriptor* HYD;
            static IEventDescriptor* EMERCANCEL;
            static IEventDescriptor* CABPRESS;
            static IEventDescriptor* RCL;
            static IEventDescriptor* STS;
            static IEventDescriptor* TOCONFIG;
            static IEventDescriptor* WHEEL;
            static IEventDescriptor* AIRDATACAPT3;
            static IEventDescriptor* AIRDATAFO3;
            static IEventDescriptor* AIRDATANORM;
            static IEventDescriptor* ATTHDGCAPT3;
            static IEventDescriptor* ATTHDGFO3;
            static IEventDescriptor* ATTHDGNORM;
            static IEventDescriptor* ECAMNDXFRCAPT;
            static IEventDescriptor* ECAMNDXFRFO;
            static IEventDescriptor* ECAMNDXFRNORM;
            static IEventDescriptor* EISDMCXFRCAPT3;
            static IEventDescriptor* EISDMCXFRFO3;
            static IEventDescriptor* EISDMCXFRNORM;
        };

        class TRANSPONDER
        {
        public:
            static IEventDescriptor* ATC1;
            static IEventDescriptor* ATC2;
            static IEventDescriptor* ALTRPTGOFF;
            static IEventDescriptor* ALTRPTGON;
            static IEventDescriptor* IDENT;
            static IEventDescriptor* KEY0;
            static IEventDescriptor* KEY1;
            static IEventDescriptor* KEY2;
            static IEventDescriptor* KEY3;
            static IEventDescriptor* KEY4;
            static IEventDescriptor* KEY5;
            static IEventDescriptor* KEY6;
            static IEventDescriptor* KEY7;
            static IEventDescriptor* KEYCLR;
            static IEventDescriptor* TRANSPONDERAUTO;
            static IEventDescriptor* TRANSPONDERON;
            static IEventDescriptor* TRANSPONDERSTBY;
            static IEventDescriptor* TCASABV;
            static IEventDescriptor* TCASALL;
            static IEventDescriptor* TCASBLW;
            static IEventDescriptor* TCASSTBY;
            static IEventDescriptor* TCASTAONLY;
            static IEventDescriptor* TCASTARA;
            static IEventDescriptor* TCASTHRT;
        };

        class ACP2
        {
        public:
            static IEventDescriptor* CABATT;
            static IEventDescriptor* HF1CALL;
            static IEventDescriptor* HF2CALL;
            static IEventDescriptor* INT;
            static IEventDescriptor* INTMECH;
            static IEventDescriptor* INTRAD;
            static IEventDescriptor* ONVOICE;
            static IEventDescriptor* PACALL;
            static IEventDescriptor* RESET;
            static IEventDescriptor* VHF1CALL;
            static IEventDescriptor* VHF2CALL;
            static IEventDescriptor* VHF3CALL;
        };

        class ACP1
        {
        public:
            static IEventDescriptor* CABATT;
            static IEventDescriptor* HF1CALL;
            static IEventDescriptor* HF2CALL;
            static IEventDescriptor* INT;
            static IEventDescriptor* INTMECH;
            static IEventDescriptor* INTRAD;
            static IEventDescriptor* ONVOICE;
            static IEventDescriptor* PACALL;
            static IEventDescriptor* RESET;
            static IEventDescriptor* VHF1CALL;
            static IEventDescriptor* VHF2CALL;
            static IEventDescriptor* VHF3CALL;
        };

    };

    class MIP
    {
    public:
        static IEventDescriptor* ANTISKID;
        static IEventDescriptor* BRKFAN;
        static IEventDescriptor* AUTOBRAKELO;
        static IEventDescriptor* AUTOBRAKEMAX;
        static IEventDescriptor* AUTOBRAKEMED;
        static IEventDescriptor* GPWSGSCS;
        static IEventDescriptor* PFDXFRCS;
        static IEventDescriptor* GPWSGSFO;
        static IEventDescriptor* PFDXFRFO;
        static IEventDescriptor* ISISBUGS;
        static IEventDescriptor* ISISLS;
        static IEventDescriptor* ISISMINUS;
        static IEventDescriptor* ISISPLUS;
        static IEventDescriptor* ISISRST;
        static IEventDescriptor* ISISBAROPUSH;
        static IEventDescriptor* LANDINGGEARDOWN;
        static IEventDescriptor* LANDINGGEARUP;
        static IEventDescriptor* TERRAINONNDCS;
        static IEventDescriptor* TERRAINONNDFO;
    };

    class OVHD
    {
    public:
        class PUSHBACK
        {
        public:
            static IEventDescriptor* FACELEFT;
            static IEventDescriptor* STRAIGHTBACK;
            static IEventDescriptor* FACERIGHT;
            static IEventDescriptor* PUSHBACKSTOP;
        };

        class REFUEL
        {
        public:
            static IEventDescriptor* POWER;
            static IEventDescriptor* REFUELING;
            static IEventDescriptor* DECREASE;
            static IEventDescriptor* INCREASE;
        };

        class ENG
        {
        public:
            static IEventDescriptor* ENG1MANSTART;
            static IEventDescriptor* ENG2MANSTART;
            static IEventDescriptor* ENG1N1MODE;
            static IEventDescriptor* ENG2N1MODE;
        };

        class HYD
        {
        public:
            static IEventDescriptor* BLUEELECPUMP;
            static IEventDescriptor* YELLOWELECPUMP;
            static IEventDescriptor* ENG1GREENPUMP;
            static IEventDescriptor* ENG2YELLOWPUMP;
            static IEventDescriptor* PTU;
            static IEventDescriptor* RATMANON;
        };

        class FIRE
        {
        public:
            static IEventDescriptor* APUAGENT;
            static IEventDescriptor* APUFIREDOWN;
            static IEventDescriptor* APUFIREUP;
            static IEventDescriptor* APUFIRETEST;
            static IEventDescriptor* ENG1FIREDOWN;
            static IEventDescriptor* ENG1FIREUP;
            static IEventDescriptor* ENG2FIREDOWN;
            static IEventDescriptor* ENG2FIREUP;
            static IEventDescriptor* ENG1AGENT1;
            static IEventDescriptor* ENG1AGENT2;
            static IEventDescriptor* ENG1FIRETEST;
            static IEventDescriptor* ENG2AGENT1;
            static IEventDescriptor* ENG2AGENT2;
            static IEventDescriptor* ENG2FIRE;
            static IEventDescriptor* ENG2FIRETEST;
        };

        class VENTILATION
        {
        public:
            static IEventDescriptor* BLOWER;
            static IEventDescriptor* CABFANS;
            static IEventDescriptor* EXTRACT;
        };

        class SIGNS
        {
        public:
            static IEventDescriptor* EMEREXITLTOFF;
            static IEventDescriptor* EMEREXITLTON;
            static IEventDescriptor* SMOKINGSIGNOFF;
            static IEventDescriptor* SMOKINGSIGNON;
            static IEventDescriptor* SEATBELTSSIGNOFF;
            static IEventDescriptor* SEATBELTSSIGNON;
        };

        class RMP3
        {
        public:
            static IEventDescriptor* ADF;
            static IEventDescriptor* AM;
            static IEventDescriptor* BFO;
            static IEventDescriptor* HF1;
            static IEventDescriptor* HF2;
            static IEventDescriptor* ILS;
            static IEventDescriptor* MLS;
            static IEventDescriptor* NAV;
            static IEventDescriptor* OFF;
            static IEventDescriptor* ON;
            static IEventDescriptor* SWAPSTBYACTIVE;
            static IEventDescriptor* VHF1;
            static IEventDescriptor* VHF2;
            static IEventDescriptor* VHF3;
            static IEventDescriptor* VOR;
        };

        class RCDR
        {
        public:
            static IEventDescriptor* CVRERASE;
            static IEventDescriptor* CVRTEST;
            static IEventDescriptor* GNDCTL;
        };

        class AIRCOND
        {
        public:
            static IEventDescriptor* PACKFLOWHI;
            static IEventDescriptor* PACKFLOWLO;
            static IEventDescriptor* PACKFLOWNORM;
            static IEventDescriptor* XBLEEDAUTO;
            static IEventDescriptor* XBLEEDOPEN;
            static IEventDescriptor* XBLEEDSHUT;
            static IEventDescriptor* APUBLEED;
            static IEventDescriptor* ENG1BLEED;
            static IEventDescriptor* ENG2BLEED;
            static IEventDescriptor* HOTAIR;
            static IEventDescriptor* PACK1;
            static IEventDescriptor* PACK2;
            static IEventDescriptor* RAMAIR;
        };

        class OXYGEN
        {
        public:
            static IEventDescriptor* CREWSUPPLY;
            static IEventDescriptor* HIGHALTLANDING;
            static IEventDescriptor* MASKMANON;
            static IEventDescriptor* PASSENGER;
        };

        class AFT
        {
        public:
            static IEventDescriptor* AVIONICSCOMPLT;
            static IEventDescriptor* OXYGENTMRRESET;
            static IEventDescriptor* SVCEINTOVRD;
            static IEventDescriptor* RESET;
            static IEventDescriptor* TEST;
            static IEventDescriptor* AUDIOSWITCHINGCAPT;
            static IEventDescriptor* AUDIOSWITCHINGFO;
            static IEventDescriptor* AUDIOSWITCHINGNORM;
            static IEventDescriptor* FADEC1;
            static IEventDescriptor* FADEC2;
            static IEventDescriptor* BLUEPUMPOVRD;
            static IEventDescriptor* LEAKB;
            static IEventDescriptor* LEAKG;
            static IEventDescriptor* LEAKY;
        };

        class GPWS
        {
        public:
            static IEventDescriptor* FLAPMODE;
            static IEventDescriptor* GSMODE;
            static IEventDescriptor* LDGFLAP3;
            static IEventDescriptor* SYS;
            static IEventDescriptor* TERR;
        };

        class FUEL
        {
        public:
            static IEventDescriptor* LTK1;
            static IEventDescriptor* LTK2;
            static IEventDescriptor* FUELMODESELECTOR;
            static IEventDescriptor* CTK1;
            static IEventDescriptor* CTK2;
            static IEventDescriptor* RTK1;
            static IEventDescriptor* RTK2;
            static IEventDescriptor* XFEED;
        };

        class FCTL
        {
        public:
            static IEventDescriptor* ELAC2;
            static IEventDescriptor* ELAC1;
            static IEventDescriptor* FAC1;
            static IEventDescriptor* FAC2;
            static IEventDescriptor* SEC2;
            static IEventDescriptor* SEC3;
            static IEventDescriptor* SEC1;
        };

        class EXTLT
        {
        public:
            static IEventDescriptor* BEACONLIGHTSOFF;
            static IEventDescriptor* BEACONLIGHTSON;
            static IEventDescriptor* LEFTLANDINGLIGHTON;
            static IEventDescriptor* LEFTLANDINGLIGHTRETRACTED;
            static IEventDescriptor* RIGHTLANDINGLIGHTON;
            static IEventDescriptor* RIGHTLANDINGLIGHTRETRACTED;
            static IEventDescriptor* NAVLOGOLIGHTSOFF;
            static IEventDescriptor* NAVLOGOLIGHTSON;
            static IEventDescriptor* NOSELIGHTOFF;
            static IEventDescriptor* NOSELIGHTTO;
            static IEventDescriptor* RWYLIGHTSOFF;
            static IEventDescriptor* RWYLIGHTSON;
            static IEventDescriptor* STROBESLIGHTSOFF;
            static IEventDescriptor* STROBESLIGHTSON;
            static IEventDescriptor* WINGLIGHTSOFF;
            static IEventDescriptor* WINGLIGHTSON;
        };

        class EVAC
        {
        public:
            static IEventDescriptor* CAPT;
            static IEventDescriptor* CAPTPURS;
            static IEventDescriptor* COMMAND;
            static IEventDescriptor* HORNSHUTOFF;
        };

        class EMERELEC
        {
        public:
            static IEventDescriptor* EMERGENTEST;
            static IEventDescriptor* GEN1LINE;
            static IEventDescriptor* MANON;
            static IEventDescriptor* RATEMERGEN;
        };

        class ELEC
        {
        public:
            static IEventDescriptor* ACESSFEED;
            static IEventDescriptor* APUGEN;
            static IEventDescriptor* BAT1;
            static IEventDescriptor* BAT2;
            static IEventDescriptor* BUSTIE;
            static IEventDescriptor* EXTPWR;
            static IEventDescriptor* GALLEY;
            static IEventDescriptor* GEN1;
            static IEventDescriptor* GEN2;
            static IEventDescriptor* IDG1;
            static IEventDescriptor* IDG2;
        };

        class CARGOSMOKE
        {
        public:
            static IEventDescriptor* AFT;
            static IEventDescriptor* AFTDISCH;
            static IEventDescriptor* FWDDISCH;
            static IEventDescriptor* FWD;
            static IEventDescriptor* TEST;
        };

        class CARGOHEAT
        {
        public:
            static IEventDescriptor* AFTISOLVALVE;
            static IEventDescriptor* FWDISOLVALVE;
            static IEventDescriptor* HOTAIR;
        };

        class CALLS
        {
        public:
            static IEventDescriptor* AFT;
            static IEventDescriptor* EMER;
            static IEventDescriptor* FWD;
            static IEventDescriptor* MECH;
        };

        class CABINPRESS
        {
        public:
            static IEventDescriptor* DITCHING;
            static IEventDescriptor* MANVSCTLDOWN;
            static IEventDescriptor* MANVSCTLUP;
            static IEventDescriptor* MODESEL;
            static IEventDescriptor* LDGELEVAUTO;
        };

        class APU
        {
        public:
            static IEventDescriptor* MASTERSW;
            static IEventDescriptor* STARTSW;
        };

        class ANTIICE
        {
        public:
            static IEventDescriptor* ENG1ANTIICE;
            static IEventDescriptor* ENG2ANTIICE;
            static IEventDescriptor* WINDOWSPROBEHEAT;
            static IEventDescriptor* WINGANTIICE;
        };

        class ADIRS
        {
        public:
            static IEventDescriptor* ADR1;
            static IEventDescriptor* ADR2;
            static IEventDescriptor* ADR3;
            static IEventDescriptor* IR1ATT;
            static IEventDescriptor* IR1NAV;
            static IEventDescriptor* IR1OFF;
            static IEventDescriptor* IR2ATT;
            static IEventDescriptor* IR2NAV;
            static IEventDescriptor* IR2OFF;
            static IEventDescriptor* IR3ATT;
            static IEventDescriptor* IR3NAV;
            static IEventDescriptor* IR3OFF;
            static IEventDescriptor* DISPLAYHDG;
            static IEventDescriptor* DISPLAYPPOS;
            static IEventDescriptor* DISPLAYSTS;
            static IEventDescriptor* DISPLAYTEST;
            static IEventDescriptor* DISPLAYTKGS;
            static IEventDescriptor* DISPLAYWIND;
            static IEventDescriptor* DISPLAYSYS1;
            static IEventDescriptor* DISPLAYSYS2;
            static IEventDescriptor* DISPLAYSYS3;
            static IEventDescriptor* DISPLAYSYSOFF;
            static IEventDescriptor* KEYPAD0;
            static IEventDescriptor* KEYPAD1;
            static IEventDescriptor* KEYPAD2;
            static IEventDescriptor* KEYPAD3;
            static IEventDescriptor* KEYPAD4;
            static IEventDescriptor* KEYPAD5;
            static IEventDescriptor* KEYPAD6;
            static IEventDescriptor* KEYPAD7;
            static IEventDescriptor* KEYPAD8;
            static IEventDescriptor* KEYPAD9;
            static IEventDescriptor* KEYPADCLR;
            static IEventDescriptor* KEYPADENTER;
        };

        class ACP3
        {
        public:
            static IEventDescriptor* CABATT;
            static IEventDescriptor* HF1CALL;
            static IEventDescriptor* HF2CALL;
            static IEventDescriptor* INT;
            static IEventDescriptor* INTMECH;
            static IEventDescriptor* INTRAD;
            static IEventDescriptor* ONVOICE;
            static IEventDescriptor* PACALL;
            static IEventDescriptor* RESET;
            static IEventDescriptor* VHF1CALL;
            static IEventDescriptor* VHF2CALL;
            static IEventDescriptor* VHF3CALL;
        };

        class INTLT
        {
        public:
            static IEventDescriptor* STBYCOMPASSOFF;
            static IEventDescriptor* ANNLTDIM;
            static IEventDescriptor* ANNLTTEST;
            static IEventDescriptor* DOMEBRT;
            static IEventDescriptor* DOMEOFF;
            static IEventDescriptor* STBYCOMPASSON;
        };

    };

    class TQ
    {
    public:
        static IEventDescriptor* ATHR1OFF;
        static IEventDescriptor* ATHR2OFF;
    };

};

class Encoders
{
public:
    class OVHD
    {
    public:
        class RMP3
        {
        public:
            static IEventDescriptor* kHz;
            static IEventDescriptor* MHz;
        };

    };

    class PEDESTAL
    {
    public:
        class RMP2
        {
        public:
            static IEventDescriptor* kHz;
            static IEventDescriptor* MHz;
        };

        class RMP1
        {
        public:
            static IEventDescriptor* kHz;
            static IEventDescriptor* MHz;
        };

    };

    class MIP
    {
    public:
        static IEventDescriptor* ISISQNH;
    };

    class GLARE
    {
    public:
        class FCU
        {
        public:
            static IEventDescriptor* ALTITUDE;
            static IEventDescriptor* HDG;
            static IEventDescriptor* SPD;
            static IEventDescriptor* VS;
        };

        class EFIS
        {
        public:
            class FO
            {
            public:
                static IEventDescriptor* ALTPRESSURE;
            };

            class CS
            {
            public:
                static IEventDescriptor* ALTPRESSURE;
            };

        };

    };

};

class Axes
{
public:
    class MIP
    {
    public:
        static IEventDescriptor* CSNDBRIGHTNESS;
        static IEventDescriptor* CSPFDBRIGHTNESS;
        static IEventDescriptor* FONDBRIGHTNESS;
        static IEventDescriptor* FOPFDBRIGHTNESS;
        static IEventDescriptor* ISISBRIGHTNESS;
    };

    class OVHD
    {
    public:
        class CARGOHEAT
        {
        public:
            static IEventDescriptor* AFTCARGOTEMPERATURE;
            static IEventDescriptor* FWDCARGOTEMPERATURE;
        };

        class AIRCOND
        {
        public:
            static IEventDescriptor* AFTTEMPERATURE;
            static IEventDescriptor* FWDTEMPERATURE;
            static IEventDescriptor* COCKPITTEMPERATURE;
        };

        class CABINPRESS
        {
        public:
            static IEventDescriptor* LANDINGELEVANALOG;
        };

        class ACP3
        {
        public:
            static IEventDescriptor* ADF1VOLUME;
            static IEventDescriptor* ADF2VOLUME;
            static IEventDescriptor* CABVOLUME;
            static IEventDescriptor* HF1VOLUME;
            static IEventDescriptor* HF2VOLUME;
            static IEventDescriptor* ILSVOLUME;
            static IEventDescriptor* INTVOLUME;
            static IEventDescriptor* MKRVOLUME;
            static IEventDescriptor* MLSVOLUME;
            static IEventDescriptor* PAVOLUME;
            static IEventDescriptor* VHF1VOLUME;
            static IEventDescriptor* VHF2VOLUME;
            static IEventDescriptor* VHF3VOLUME;
            static IEventDescriptor* VOR1VOLUME;
            static IEventDescriptor* VOR2VOLUME;
        };

    };

    class PEDESTAL
    {
    public:
        class TQ
        {
        public:
            static IEventDescriptor* ELEVATORTRIMANALOG;
            static IEventDescriptor* ENG1LEVER;
            static IEventDescriptor* ENG2LEVER;
        };

        class LOWER
        {
        public:
            static IEventDescriptor* FLAPS;
            static IEventDescriptor* GRAVITYGEAR;
            static IEventDescriptor* SPEEDBRAKE;
        };

        class ECAM
        {
        public:
            static IEventDescriptor* RADARTILT;
            static IEventDescriptor* RADARGAIN;
            static IEventDescriptor* LOWERDISPLAY;
            static IEventDescriptor* UPPERDISPLAY;
        };

        class ACP2
        {
        public:
            static IEventDescriptor* ADF1VOLUME;
            static IEventDescriptor* ADF2VOLUME;
            static IEventDescriptor* CABVOLUME;
            static IEventDescriptor* HF1VOLUME;
            static IEventDescriptor* HF2VOLUME;
            static IEventDescriptor* ILSVOLUME;
            static IEventDescriptor* INTVOLUME;
            static IEventDescriptor* MKRVOLUME;
            static IEventDescriptor* MLSVOLUME;
            static IEventDescriptor* PAVOLUME;
            static IEventDescriptor* VHF1VOLUME;
            static IEventDescriptor* VHF2VOLUME;
            static IEventDescriptor* VHF3VOLUME;
            static IEventDescriptor* VOR1VOLUME;
            static IEventDescriptor* VOR2VOLUME;
        };

        class ACP1
        {
        public:
            static IEventDescriptor* ADF1VOLUME;
            static IEventDescriptor* ADF2VOLUME;
            static IEventDescriptor* CABVOLUME;
            static IEventDescriptor* HF1VOLUME;
            static IEventDescriptor* HF2VOLUME;
            static IEventDescriptor* ILSVOLUME;
            static IEventDescriptor* INTVOLUME;
            static IEventDescriptor* MKRVOLUME;
            static IEventDescriptor* MLSVOLUME;
            static IEventDescriptor* PAVOLUME;
            static IEventDescriptor* VHF1VOLUME;
            static IEventDescriptor* VHF2VOLUME;
            static IEventDescriptor* VHF3VOLUME;
            static IEventDescriptor* VOR1VOLUME;
            static IEventDescriptor* VOR2VOLUME;
        };

    };

};