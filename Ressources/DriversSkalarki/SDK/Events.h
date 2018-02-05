
#pragma once

enum class Events
{
    /// <summary>
    /// The A/THR disconnect button on the left throttle lever has changed state.
    /// </summary>
    ATHR1OFF,
    /// <summary>
    /// The A/THR disconnect button on the right throttle lever has changed state.
    /// </summary>
    ATHR2OFF,
    /// <summary>
    /// The standby compass off switch has changed state.
    /// </summary>
    STBYCOMPASSOFF,
    /// <summary>
    /// The ANTISKID switch has changed state.
    /// </summary>
    ANTISKID,
    /// <summary>
    /// The BRKFAN  switch has changed state.
    /// </summary>
    BRKFAN,
    /// <summary>
    /// The AUTOBRAKELO switch has changed state.
    /// </summary>
    AUTOBRAKELO,
    /// <summary>
    /// The AUTOBRAKEMAX switch has changed state.
    /// </summary>
    AUTOBRAKEMAX,
    /// <summary>
    /// The AUTOBRAKEMED switch has changed state.
    /// </summary>
    AUTOBRAKEMED,
    /// <summary>
    /// The CABATT switch has changed state.
    /// </summary>
    CABATT,
    /// <summary>
    /// The HF1CALL switch has changed state.
    /// </summary>
    HF1CALL,
    /// <summary>
    /// The HF2CALL switch has changed state.
    /// </summary>
    HF2CALL,
    /// <summary>
    /// The INT switch has changed state.
    /// </summary>
    INT,
    /// <summary>
    /// The INTMECH switch has changed state.
    /// </summary>
    INTMECH,
    /// <summary>
    /// The INTRAD switch has changed state.
    /// </summary>
    INTRAD,
    /// <summary>
    /// The ONVOICE switch has changed state.
    /// </summary>
    ONVOICE,
    /// <summary>
    /// The PACALL switch has changed state.
    /// </summary>
    PACALL,
    /// <summary>
    /// The RESET (ACP) switch has changed state.
    /// </summary>
    RESET,
    /// <summary>
    /// The VHF1CALL switch has changed state.
    /// </summary>
    VHF1CALL,
    /// <summary>
    /// The VHF2CALL switch has changed state.
    /// </summary>
    VHF2CALL,
    /// <summary>
    /// The VHF3CALL switch has changed state.
    /// </summary>
    VHF3CALL,
    /// <summary>
    /// The ADR1 switch has changed state.
    /// </summary>
    ADR1,
    /// <summary>
    /// The ADR2 switch has changed state.
    /// </summary>
    ADR2,
    /// <summary>
    /// The ADR3 switch has changed state.
    /// </summary>
    ADR3,
    /// <summary>
    /// The IR1ATT switch has changed state.
    /// </summary>
    IR1ATT,
    /// <summary>
    /// The IR2NAV switch has changed state.
    /// </summary>
    IR1NAV,
    /// <summary>
    /// The IR1OFF switch has changed state.
    /// </summary>
    IR1OFF,
    /// <summary>
    /// The IR2ATT switch has changed state.
    /// </summary>
    IR2ATT,
    /// <summary>
    /// The IR2NAV switch has changed state.
    /// </summary>
    IR2NAV,
    /// <summary>
    /// IR2OFF switch has changed state.
    /// </summary>
    IR2OFF,
    /// <summary>
    /// The IR3ATT switch has changed state.
    /// </summary>
    IR3ATT,
    /// <summary>
    /// IR3NAV switch has changed state.
    /// </summary>
    IR3NAV,
    /// <summary>
    /// The IR3OFF switch has changed state.
    /// </summary>
    IR3OFF,
    /// <summary>
    /// The DISPLAYHDG switch has changed state.
    /// </summary>
    DISPLAYHDG,
    /// <summary>
    /// The DISPLAYPPOS switch has changed state.
    /// </summary>
    DISPLAYPPOS,
    /// <summary>
    /// The DISPLAYSTS switch has changed state.
    /// </summary>
    DISPLAYSTS,
    /// <summary>
    /// The DISPLAYTEST switch has changed state.
    /// </summary>
    DISPLAYTEST,
    /// <summary>
    /// The DISPLAYTKGS switch has changed state.
    /// </summary>
    DISPLAYTKGS,
    /// <summary>
    /// The DISPLAYWIND  switch has changed state.
    /// </summary>
    DISPLAYWIND,
    /// <summary>
    /// The DISPLAYSYS1 switch has changed state.
    /// </summary>
    DISPLAYSYS1,
    /// <summary>
    /// The DISPLAYSYS2 switch has changed state.
    /// </summary>
    DISPLAYSYS2,
    /// <summary>
    /// The DISPLAYSYS3 switch has changed state.
    /// </summary>
    DISPLAYSYS3,
    /// <summary>
    /// The DISPLAYSYSOFF switch has changed state.
    /// </summary>
    DISPLAYSYSOFF,
    /// <summary>
    /// The KEYPAD0 switch has changed state.
    /// </summary>
    KEYPAD0,
    /// <summary>
    /// The KEYPAD1 switch has changed state.
    /// </summary>
    KEYPAD1,
    /// <summary>
    /// The KEYPAD2 switch has changed state.
    /// </summary>
    KEYPAD2,

    /// <summary>
    /// The KEYPAD3 switch has changed state.
    /// </summary>
    KEYPAD3,
    /// <summary>
    /// The KEYPAD4 switch has changed state.
    /// </summary>
    KEYPAD4,
    /// <summary>
    /// The KEYPAD5 switch has changed state.
    /// </summary>
    KEYPAD5,
    /// <summary>
    /// The KEYPAD6 switch has changed state.
    /// </summary>
    KEYPAD6,
    /// <summary>
    /// The KEYPAD7 switch has changed state.
    /// </summary>
    KEYPAD7,
    /// <summary>
    /// The KEYPAD8 switch has changed state.
    /// </summary>
    KEYPAD8,
    /// <summary>
    /// The KEYPAD9 switch has changed state.
    /// </summary>
    KEYPAD9,
    /// <summary>
    /// The KEYPADCLR switch has changed state.
    /// </summary>
    KEYPADCLR,
    /// <summary>
    /// The KEYPADENTER switch has changed state.
    /// </summary>
    KEYPADENTER,
    /// <summary>
    /// The ENG1ANTIICE switch has changed state.
    /// </summary>
    ENG1ANTIICE,
    /// <summary>
    /// The ENG2ANTIICE switch has changed state.
    /// </summary>
    ENG2ANTIICE,
    /// <summary>
    /// The WINDOWSPROBEHEAT switch has changed state.
    /// </summary>
    WINDOWSPROBEHEAT,
    /// <summary>
    /// The WINGANTIICE switch has changed state.
    /// </summary>
    WINGANTIICE,

    /// <summary>
    /// The MASTERSW switch has changed state.
    /// </summary>
    MASTERSW,

    /// <summary>
    /// The STARTSW switch has changed state.
    /// </summary>
    STARTSW,

    /// <summary>
    /// The IDENT switch has changed state.
    /// </summary>
    IDENT,

    /// <summary>
    /// The KEY0 (transponder) switch has changed state.
    /// </summary>
    KEY0,
    /// <summary>
    /// The KEY1 (transponder) switch has changed state.
    /// </summary>
    KEY1,
    /// <summary>
    /// The KEY2 (transponder) switch has changed state.
    /// </summary>
    KEY2,
    /// <summary>
    /// The KEY3 (transponder) switch has changed state.
    /// </summary>
    KEY3,
    /// <summary>
    /// The KEY4 (transponder) switch has changed state.
    /// </summary>
    KEY4,
    /// <summary>
    /// The KEY5 (transponder) switch has changed state.
    /// </summary>
    KEY5,
    /// <summary>
    /// The KEY6 (transponder) switch has changed state.
    /// </summary>
    KEY6,
    /// <summary>
    /// The KEY7 (transponder) switch has changed state.
    /// </summary>
    KEY7,
    /// <summary>
    /// The KEYCLR (transponder) switch has changed state.
    /// </summary>
    KEYCLR,

    /// <summary>
    /// The TRANSPONDERAUTO switch has changed state.
    /// </summary>
    TRANSPONDERAUTO,

    /// <summary>
    /// The TRANSPONDERON switch has changed state.
    /// </summary>
    TRANSPONDERON,

    /// <summary>
    /// The TRANSPONDERSTBY switch has changed state.
    /// </summary>
    TRANSPONDERSTBY,

    /// <summary>
    /// The DITCHING switch has changed state.
    /// </summary>
    DITCHING,

    /// <summary>
    /// The MANVSCTLDOWN switch has changed state.
    /// </summary>
    MANVSCTLDOWN,

    /// <summary>
    /// The MANVSCTLUP switch has changed state.
    /// </summary>
    MANVSCTLUP,

    /// <summary>
    /// The MODESEL switch has changed state.
    /// </summary>
    MODESEL,

    /// <summary>
    /// The LDGELEVAUTO switch has changed state.
    /// </summary>
    LDGELEVAUTO,

    /// <summary>
    /// The AFT switch has changed state.
    /// </summary>
    AFT,

    /// <summary>
    /// The EMER switch has changed state.
    /// </summary>
    EMER,

    /// <summary>
    /// The FDW switch has changed state.
    /// </summary>
    FWD,

    /// <summary>
    /// The MECH switch has changed state.
    /// </summary>
    MECH,

    /// <summary>
    /// The AFTISOLVALVE switch has changed state.
    /// </summary>
    AFTISOLVALVE,

    /// <summary>
    /// The FWDISOLVALVE switch has changed state.
    /// </summary>
    FWDISOLVALVE,

    /// <summary>
    /// The HOTAIR switch has changed state.
    /// </summary>
    HOTAIR,

    /// <summary>
    /// The AFTDISCH switch has changed state.
    /// </summary>
    AFTDISCH,

    /// <summary>
    /// The FWDDISCH switch has changed state.
    /// </summary>
    FWDDISCH,

    /// <summary>
    /// The TEST switch has changed state.
    /// </summary>
    TEST,

    /// <summary>
    /// The ALL switch has changed state.
    /// </summary>
    ALL,

    /// <summary>
    /// The APU switch has changed state.
    /// </summary>
    APU,

    /// <summary>
    /// The BLEED switch has changed state.
    /// </summary>
    BLEED,

    /// <summary>
    /// The CLR1 (left hand side) ECAM switch has changed state.
    /// </summary>
    CLR1,

    /// <summary>
    /// The CLR2 (right hand side) ECAM switch has changed state.
    /// </summary>
    CLR2,

    /// <summary>
    /// The COND switch has changed state.
    /// </summary>
    COND,

    /// <summary>
    /// The DOOR switch has changed state.
    /// </summary>
    DOOR,

    /// <summary>
    /// The ELEC switch has changed state.
    /// </summary>
    ELEC,

    /// <summary>
    /// The ENG switch has changed state.
    /// </summary>
    ENG,

    /// <summary>
    /// The FCTL switch has changed state.
    /// </summary>
    FCTL,

    /// <summary>
    /// The FUEL switch has changed state.
    /// </summary>
    FUEL,

    /// <summary>
    /// The HYD switch has changed state.
    /// </summary>
    HYD,

    /// <summary>
    /// The CABPRESS switch has changed state.
    /// </summary>
    CABPRESS,

    /// <summary>
    /// The RCL switch has changed state.
    /// </summary>
    RCL,

    /// <summary>
    /// The STS switch has changed state.
    /// </summary>
    STS,

    /// <summary>
    /// The TOCONFIG switch has changed state.
    /// </summary>
    TOCONFIG,

    /// <summary>
    /// The WHEEL switch has changed state.
    /// </summary>
    WHEEL,

    /// <summary>
    /// The ARPT (EFIS) switch has changed state.
    /// </summary>
    ARPT,

    /// <summary>
    /// The CSTR (EFIS) switch has changed state.
    /// </summary>
    CSTR,

    /// <summary>
    /// The FD (EFIS) switch has changed state.
    /// </summary>
    FD,

    /// <summary>
    /// The QNHMB (EFIS) switch has changed state.
    /// </summary>
    QNHMB,

    /// <summary>
    /// The ILS (EFIS) switch has changed state.
    /// </summary>
    ILS,

    /// <summary>
    /// The QNHINHG (EFIS) switch has changed state.
    /// </summary>
    QNHINHG,

    /// <summary>
    /// The KOHLSMANPULL (pressure altitude) (EFIS) switch has changed state.
    /// </summary>
    KOHLSMANPULL,

    /// <summary>
    /// The KOHLSMANPUSH (pressure altitude) (EFIS) switch has changed state.
    /// </summary>
    KOHLSMANPUSH,

    /// <summary>
    /// The NDARCMODE (EFIS) switch has changed state.
    /// </summary>
    NDARCMODE,

    /// <summary>
    /// The NDILSMODE (EFIS) switch has changed state.
    /// </summary>
    NDILSMODE,

    /// <summary>
    /// The NDNAVMODE (EFIS) switch has changed state.
    /// </summary>
    NDNAVMODE,

    /// <summary>
    /// The NDPLANMODE (EFIS) switch has changed state.
    /// </summary>
    NDPLANMODE,

    /// <summary>
    /// The NDVORMODE (EFIS) switch has changed state.
    /// </summary>
    NDVORMODE,

    /// <summary>
    /// The NAV1ADF (EFIS) switch has changed state.
    /// </summary>
    NAV1ADF,

    /// <summary>
    /// The NAV1VOR (EFIS) switch has changed state.
    /// </summary>
    NAV1VOR,

    /// <summary>
    /// The NAV2ADF (EFIS) switch has changed state.
    /// </summary>
    NAV2ADF,

    /// <summary>
    /// The NAV2VOR (EFIS) switch has changed state.
    /// </summary>
    NAV2VOR,

    /// <summary>
    /// The NDB (EFIS) switch has changed state.
    /// </summary>
    NDB,

    /// <summary>
    /// The NDRANGE10 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE10,

    /// <summary>
    /// The NDRANGE160 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE160,

    /// <summary>
    /// The NDRANGE20 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE20,

    /// <summary>
    /// The NDRANGE320 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE320,

    /// <summary>
    /// The NDRANGE40 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE40,

    /// <summary>
    /// The NDRANGE80 (EFIS) switch has changed state.
    /// </summary>
    NDRANGE80,

    /// <summary>
    /// The VORD (EFIS) switch has changed state.
    /// </summary>
    VORD,

    /// <summary>
    /// The WPT (EFIS) switch has changed state.
    /// </summary>
    WPT,

    /// <summary>
    /// The GPWSGSCS switch has changed state.
    /// </summary>
    GPWSGSCS,

    /// <summary>
    /// The PFDXFRCS switch has changed state.
    /// </summary>
    PFDXFRCS,

    /// <summary>
    /// The GPWSGSFO switch has changed state.
    /// </summary>
    GPWSGSFO,

    /// <summary>
    /// The PFDXFRFO switch has changed state.
    /// </summary>
    PFDXFRFO,

    /// <summary>
    /// The KOHLSMANNPUSH switch has changed state.
    /// </summary>
    KOHLSMANNPUSH,

    /// <summary>
    /// The ACESSFEED switch has changed state.
    /// </summary>
    ACESSFEED,

    /// <summary>
    /// The APUGEN switch has changed state.
    /// </summary>
    APUGEN,

    /// <summary>
    /// The BAT1 switch has changed state.
    /// </summary>
    BAT1,

    /// <summary>
    /// The BAT2 switch has changed state.
    /// </summary>
    BAT2,

    /// <summary>
    /// The BUSTIE switch has changed state.
    /// </summary>
    BUSTIE,

    /// <summary>
    /// The EXTPWR switch has changed state.
    /// </summary>
    EXTPWR,

    /// <summary>
    /// The GALLEY switch has changed state.
    /// </summary>
    GALLEY,

    /// <summary>
    /// The GEN1 switch has changed state.
    /// </summary>
    GEN1,

    /// <summary>
    /// The GEN2 switch has changed state.
    /// </summary>
    GEN2,

    /// <summary>
    /// The IDG1 switch has changed state.
    /// </summary>
    IDG1,

    /// <summary>
    /// The IDG2 switch has changed state.
    /// </summary>
    IDG2,

    /// <summary>
    /// The EMERGENTEST switch has changed state.
    /// </summary>
    EMERGENTEST,

    /// <summary>
    /// The GEN1LINE switch has changed state.
    /// </summary>
    GEN1LINE,

    /// <summary>
    /// The MANON switch has changed state.
    /// </summary>
    MANON,

    /// <summary>
    /// The RATEMERGEN switch has changed state.
    /// </summary>
    RATEMERGEN,

    /// <summary>
    /// The CAPT switch has changed state.
    /// </summary>
    CAPT,

    /// <summary>
    /// The CAPTPURS switch has changed state.
    /// </summary>
    CAPTPURS,

    /// <summary>
    /// The COMMAND switch has changed state.
    /// </summary>
    COMMAND,

    /// <summary>
    /// The HORNSHUTOFF switch has changed state.
    /// </summary>
    HORNSHUTOFF,

    /// <summary>
    /// The BEACONLIGHTSOFF switch has changed state.
    /// </summary>
    BEACONLIGHTSOFF,

    /// <summary>
    /// The BEACONLIGHTSON switch has changed state.
    /// </summary>
    BEACONLIGHTSON,

    /// <summary>
    /// The LEFTLANDINGLIGHTON switch has changed state.
    /// </summary>
    LEFTLANDINGLIGHTON,

    /// <summary>
    /// The LEFTLANDINGLIGHTRETRACTED switch has changed state.
    /// </summary>
    LEFTLANDINGLIGHTRETRACTED,

    /// <summary>
    /// The RIGHTLANDINGLIGHTON switch has changed state.
    /// </summary>
    RIGHTLANDINGLIGHTON,

    /// <summary>
    /// The RIGHTLANDINGLIGHTRETRACTED switch has changed state.
    /// </summary>
    RIGHTLANDINGLIGHTRETRACTED,

    /// <summary>
    /// The NAVLOGOLIGHTSOFF switch has changed state.
    /// </summary>
    NAVLOGOLIGHTSOFF,

    /// <summary>
    /// The NAVLOGOLIGHTSON switch has changed state.
    /// </summary>
    NAVLOGOLIGHTSON,

    /// <summary>
    /// The NOSELIGHTOFF switch has changed state.
    /// </summary>
    NOSELIGHTOFF,

    /// <summary>
    /// The NOSELIGHTTO switch has changed state.
    /// </summary>
    NOSELIGHTTO,

    /// <summary>
    /// The RWYLIGHTSOFF switch has changed state.
    /// </summary>
    RWYLIGHTSOFF,

    /// <summary>
    /// The RWYLIGHTSON switch has changed state.
    /// </summary>
    RWYLIGHTSON,

    /// <summary>
    /// The STROBESLIGHTSOFF switch has changed state.
    /// </summary>
    STROBESLIGHTSOFF,

    /// <summary>
    /// The STROBESLIGHTSON switch has changed state.
    /// </summary>
    STROBESLIGHTSON,

    /// <summary>
    /// The WINGLIGHTSOFF switch has changed state.
    /// </summary>
    WINGLIGHTSOFF,

    /// <summary>
    /// The WINGLIGHTSON switch has changed state.
    /// </summary>
    WINGLIGHTSON,

    /// <summary>
    /// The ATHR switch has changed state.
    /// </summary>
    ATHR,

    /// <summary>
    /// The ALTITUDECHANGE100 switch has changed state.
    /// </summary>
    ALTITUDECHANGE100,

    /// <summary>
    /// The ALTITUDECHANGE1000 switch has changed state.
    /// </summary>
    ALTITUDECHANGE1000,

    /// <summary>
    /// The ALTITUDEPULL switch has changed state.
    /// </summary>
    ALTITUDEPULL,

    /// <summary>
    /// The ALTITUDEPUSH switch has changed state.
    /// </summary>
    ALTITUDEPUSH,

    /// <summary>
    /// The AP1 switch has changed state.
    /// </summary>
    AP1,

    /// <summary>
    /// The AP2 switch has changed state.
    /// </summary>
    AP2,

    /// <summary>
    /// The APPR switch has changed state.
    /// </summary>
    APPR,

    /// <summary>
    /// The EXPED switch has changed state.
    /// </summary>
    EXPED,

    /// <summary>
    /// The HDGVSTRKFPA switch has changed state.
    /// </summary>
    HDGVSTRKFPA,

    /// <summary>
    /// The HEADINGPULL switch has changed state.
    /// </summary>
    HEADINGPULL,

    /// <summary>
    /// The HEADINGPUSH switch has changed state.
    /// </summary>
    HEADINGPUSH,

    /// <summary>
    /// The LOC switch has changed state.
    /// </summary>
    LOC,

    /// <summary>
    /// The METRICALT switch has changed state.
    /// </summary>
    METRICALT,

    /// <summary>
    /// The SPDMACH switch has changed state.
    /// </summary>
    SPDMACH,

    /// <summary>
    /// The SPEEDPULL switch has changed state.
    /// </summary>
    SPEEDPULL,

    /// <summary>
    /// The SPEEDPUSH switch has changed state.
    /// </summary>
    SPEEDPUSH,

    /// <summary>
    /// The VSPULL switch has changed state.
    /// </summary>
    VSPULL,

    /// <summary>
    /// The VSPUSH switch has changed state.
    /// </summary>
    VSPUSH,

    /// <summary>
    /// The ELAC2 switch has changed state.
    /// </summary>
    ELAC2,

    /// <summary>
    /// The ELAC1 switch has changed state.
    /// </summary>
    ELAC1,

    /// <summary>
    /// The FAC1 switch has changed state.
    /// </summary>
    FAC1,

    /// <summary>
    /// The FAC2 switch has changed state.
    /// </summary>
    FAC2,

    /// <summary>
    /// The SEC2 switch has changed state.
    /// </summary>
    SEC2,

    /// <summary>
    /// The SEC3 switch has changed state.
    /// </summary>
    SEC3,

    /// <summary>
    /// The SEC1 switch has changed state.
    /// </summary>
    SEC1,

    /// <summary>
    /// The LTK1 switch has changed state.
    /// </summary>
    LTK1,

    /// <summary>
    /// The LTK2 switch has changed state.
    /// </summary>
    LTK2,

    /// <summary>
    /// The FUELMODESELECTOR switch has changed state.
    /// </summary>
    FUELMODESELECTOR,

    /// <summary>
    /// The CTK1 switch has changed state.
    /// </summary>
    CTK1,

    /// <summary>
    /// The CTK2 switch has changed state.
    /// </summary>
    CTK2,
    /// <summary>
    /// The RTK2 switch has changed state.
    /// </summary>
    RTK1,
    /// <summary>
    /// The RTK2 switch has changed state.
    /// </summary>
    RTK2,

    /// <summary>
    /// The XFEED switch has changed state.
    /// </summary>
    XFEED,

    /// <summary>
    /// The FLAPMODE switch has changed state.
    /// </summary>
    FLAPMODE,

    /// <summary>
    /// The GSMODE switch has changed state.
    /// </summary>
    GSMODE,

    /// <summary>
    /// The LDGFLAP3 switch has changed state.
    /// </summary>
    LDGFLAP3,

    /// <summary>
    /// The SYS switch has changed state.
    /// </summary>
    SYS,

    /// <summary>
    /// The TERR switch has changed state.
    /// </summary>
    TERR,

    /// <summary>
    /// The ANNLTDIM switch has changed state.
    /// </summary>
    ANNLTDIM,

    /// <summary>
    /// The ANNLTTEST switch has changed state.
    /// </summary>
    ANNLTTEST,

    /// <summary>
    /// The DOMEBRT switch has changed state.
    /// </summary>
    DOMEBRT,

    /// <summary>
    /// The DOMEOFF switch has changed state.
    /// </summary>
    DOMEOFF,

    /// <summary>
    /// The STBYCOMPASSON switch has changed state.
    /// </summary>
    STBYCOMPASSON,

    /// <summary>
    /// The ISISBUGS switch has changed state.
    /// </summary>
    ISISBUGS,

    /// <summary>
    /// The ISISLS switch has changed state.
    /// </summary>
    ISISLS,

    /// <summary>
    /// The ISISMINUS switch has changed state.
    /// </summary>
    ISISMINUS,

    /// <summary>
    /// The ISISPLUS switch has changed state.
    /// </summary>
    ISISPLUS,

    /// <summary>
    /// The ISISRST switch has changed state.
    /// </summary>
    ISISRST,

    /// <summary>
    /// The LANDINGGEARDOWN switch has changed state.
    /// </summary>
    LANDINGGEARDOWN,

    /// <summary>
    /// The LANDINGGEARUP switch has changed state.
    /// </summary>
    LANDINGGEARUP,

    /// <summary>
    /// The TERRAINONNDCS switch has changed state.
    /// </summary>
    TERRAINONNDCS,

    /// <summary>
    /// The COCKPITDOORLOCK switch has changed state.
    /// </summary>
    COCKPITDOORLOCK,

    /// <summary>
    /// The COCKPITDOORUNLOCK switch has changed state.
    /// </summary>
    COCKPITDOORUNLOCK,

    /// <summary>
    /// The PARKBRK switch has changed state.
    /// </summary>
    PARKBRK,

    /// <summary>
    /// The RUDDERTRIMLEFT switch has changed state.
    /// </summary>
    RUDDERTRIMLEFT,

    /// <summary>
    /// The RUDDERTRIMRESET switch has changed state.
    /// </summary>
    RUDDERTRIMRESET,

    /// <summary>
    /// The RUDDERTRIMRIGHT switch has changed state.
    /// </summary>
    RUDDERTRIMRIGHT,

    /// <summary>
    /// The ARMGROUNDSPOILERS switch has changed state.
    /// </summary>
    ARMGROUNDSPOILERS,

    /// <summary>
    /// The AVIONICSCOMPLT switch has changed state.
    /// </summary>
    AVIONICSCOMPLT,

    /// <summary>
    /// The OXYGENTMRRESET switch has changed state.
    /// </summary>
    OXYGENTMRRESET,

    /// <summary>
    /// The SVCEINTOVRD switch has changed state.
    /// </summary>
    SVCEINTOVRD,

    /// <summary>
    /// The AUDIOSWITCHINGCAPT switch has changed state.
    /// </summary>
    AUDIOSWITCHINGCAPT,

    /// <summary>
    /// The AUDIOSWITCHINGFO switch has changed state.
    /// </summary>
    AUDIOSWITCHINGFO,

    /// <summary>
    /// The AUDIOSWITCHINGNORM switch has changed state.
    /// </summary>
    AUDIOSWITCHINGNORM,

    /// <summary>
    /// The FADEC1 switch has changed state.
    /// </summary>
    FADEC1,

    /// <summary>
    /// The FADEC2 switch has changed state.
    /// </summary>
    FADEC2,

    /// <summary>
    /// The BLUEPUMPOVRD switch has changed state.
    /// </summary>
    BLUEPUMPOVRD,

    /// <summary>
    /// The LEAKB switch has changed state.
    /// </summary>
    LEAKB,

    /// <summary>
    /// The LEAKG switch has changed state.
    /// </summary>
    LEAKG,

    /// <summary>
    /// The LEAKY switch has changed state.
    /// </summary>
    LEAKY,

    /// <summary>
    /// The CREWSUPPLY switch has changed state.
    /// </summary>
    CREWSUPPLY,

    /// <summary>
    /// The HIGHALTLANDING switch has changed state.
    /// </summary>
    HIGHALTLANDING,

    /// <summary>
    /// The MASKMANON switch has changed state.
    /// </summary>
    MASKMANON,

    /// <summary>
    /// The PASSENGER switch has changed state.
    /// </summary>
    PASSENGER,

    /// <summary>
    /// The PACKFLOWHI switch has changed state.
    /// </summary>
    PACKFLOWHI,

    /// <summary>
    /// The PACKFLOWLO switch has changed state.
    /// </summary>
    PACKFLOWLO,

    /// <summary>
    /// The PACKFLOWNORM switch has changed state.
    /// </summary>
    PACKFLOWNORM,

    /// <summary>
    /// The CVRERASE switch has changed state.
    /// </summary>
    CVRERASE,

    /// <summary>
    /// The CVRTEST switch has changed state.
    /// </summary>
    CVRTEST,

    /// <summary>
    /// The GNDCTL switch has changed state.
    /// </summary>
    GNDCTL,

    /// <summary>
    /// The ADF (RMP) switch has changed state.
    /// </summary>
    ADF,

    /// <summary>
    /// The AM (RMP) switch has changed state.
    /// </summary>
    AM,

    /// <summary>
    /// The BFO (RMP) switch has changed state.
    /// </summary>
    BFO,

    /// <summary>
    /// The HF1 (RMP) switch has changed state.
    /// </summary>
    HF1,

    /// <summary>
    /// The HF2 (RMP) switch has changed state.
    /// </summary>
    HF2,

    /// <summary>
    /// The MLS (RMP) switch has changed state.
    /// </summary>
    MLS,

    /// <summary>
    /// The NAV (RMP) switch has changed state.
    /// </summary>
    NAV,

    /// <summary>
    /// The OFF (RMP) switch has changed state.
    /// </summary>
    OFF,

    /// <summary>
    /// The ON (RMP) switch has changed state.
    /// </summary>
    ON,

    /// <summary>
    /// The SEL (RMP) switch has changed state.
    /// </summary>
    SEL,

    /// <summary>
    /// The SWAPSTBYACTIVE (RMP) switch has changed state.
    /// </summary>
    SWAPSTBYACTIVE,

    /// <summary>
    /// The VHF1 (RMP) switch has changed state.
    /// </summary>
    VHF1,

    /// <summary>
    /// The VHF2 (RMP) switch has changed state.
    /// </summary>
    VHF2,

    /// <summary>
    /// The VHF3 (RMP) switch has changed state.
    /// </summary>
    VHF3,

    /// <summary>
    /// The VOR (RMP) switch has changed state.
    /// </summary>
    VOR,

    /// <summary>
    /// The EMEREXITLT switch has changed state.
    /// </summary>
    EMEREXITLT,

    /// <summary>
    /// The SMOKINGSIGNOFF switch has changed state.
    /// </summary>
    SMOKINGSIGNOFF,

    /// <summary>
    /// The SMOKINGSIGNON switch has changed state.
    /// </summary>
    SMOKINGSIGNON,

    /// <summary>
    /// The SEATBELTSSIGNOFF switch has changed state.
    /// </summary>
    SEATBELTSSIGNOFF,

    /// <summary>
    /// The SEATBELTSSIGNON switch has changed state.
    /// </summary>
    SEATBELTSSIGNON,

    /// <summary>
    /// The AIRDATACAPT3 switch has changed state.
    /// </summary>
    AIRDATACAPT3,

    /// <summary>
    /// The AIRDATAFO3 switch has changed state.
    /// </summary>
    AIRDATAFO3,

    /// <summary>
    /// The AIRDATANORM switch has changed state.
    /// </summary>
    AIRDATANORM,

    /// <summary>
    /// The ATTHDGCAPT3 switch has changed state.
    /// </summary>
    ATTHDGCAPT3,

    /// <summary>
    /// The ATTHDGFO3 switch has changed state.
    /// </summary>
    ATTHDGFO3,

    /// <summary>
    /// The ATTHDGNORM switch has changed state.
    /// </summary>
    ATTHDGNORM,

    /// <summary>
    /// The ECAMNDXFRCAPT switch has changed state.
    /// </summary>
    ECAMNDXFRCAPT,

    /// <summary>
    /// The ECAMNDXFRFO switch has changed state.
    /// </summary>
    ECAMNDXFRFO,

    /// <summary>
    /// The ECAMNDXFRNORM switch has changed state.
    /// </summary>
    ECAMNDXFRNORM,

    /// <summary>
    /// The EISDMCXFRCAPT3 switch has changed state.
    /// </summary>
    EISDMCXFRCAPT3,

    /// <summary>
    /// The EISDMCXFRFO3 switch has changed state.
    /// </summary>
    EISDMCXFRFO3,

    /// <summary>
    /// The EISDMCXFRNORM switch has changed state.
    /// </summary>
    EISDMCXFRNORM,

    /// <summary>
    /// The TERRAIN ON ND FO switch has changed state.
    /// </summary>
    TERRAINONNDFO,

    /// <summary>
    /// The TCAS ABV switch has changed state.
    /// </summary>
    TCASABV,

    /// <summary>
    /// The TCAS ALL switch has changed state.
    /// </summary>
    TCASALL,

    /// <summary>
    /// The TCAS BLW switch has changed state.
    /// </summary>
    TCASBLW,

    /// <summary>
    /// The TCAS STBY switch has changed state.
    /// </summary>
    TCASSTBY,

    /// <summary>
    /// The TCAS TA ONLY switch has changed state.
    /// </summary>
    TCASTAONLY,

    /// <summary>
    /// The TCAS TA/RA switch has changed state.
    /// </summary>
    TCASTARA,

    /// <summary>
    /// The TCAS THRT switch has changed state.
    /// </summary>
    TCASTHRT,

    /// <summary>
    /// The ENGSTARTCRANK switch has changed state.
    /// </summary>
    ENGSTARTCRANK,

    /// <summary>
    /// The ENG1MASTERSWITCH switch has changed state.
    /// </summary>
    ENG1MASTERSWITCH,

    /// <summary>
    /// The ENG2MASTERSWITCH switch has changed state.
    /// </summary>
    ENG2MASTERSWITCH,

    /// <summary>
    /// The ENGSTARTIGN switch has changed state.
    /// </summary>
    ENGSTARTIGN,

    /// <summary>
    /// The ENGSTARTNORM switch has changed state.
    /// </summary>
    ENGSTARTNORM,

    /// <summary>
    /// The BLOWER switch has changed state.
    /// </summary>
    BLOWER,

    /// <summary>
    /// The CABFANS switch has changed state.
    /// </summary>
    CABFANS,

    /// <summary>
    /// The EXTRACT switch has changed state.
    /// </summary>
    EXTRACT,

    /// <summary>
    /// The AUTOLANDCS switch has changed state.
    /// </summary>
    AUTOLANDCS,

    /// <summary>
    /// The CHRONOCS switch has changed state.
    /// </summary>
    CHRONOCS,

    /// <summary>
    /// The MASTERCAUTIONCS switch has changed state.
    /// </summary>
    MASTERCAUTIONCS,

    /// <summary>
    /// The MASTERWARNINGCS switch has changed state.
    /// </summary>
    MASTERWARNINGCS,

    /// <summary>
    /// The SIDESTICKPRIORITYCS switch has changed state.
    /// </summary>
	SIDESTICKPRIORITYCS,

    /// <summary>
    /// The AUTOLANDFO switch has changed state.
    /// </summary>
    AUTOLANDFO,

    /// <summary>
    /// The CHRONOFO switch has changed state.
    /// </summary>
    CHRONOFO,

    /// <summary>
    /// The MASTERCAUTIONFO switch has changed state.
    /// </summary>
    MASTERCAUTIONFO,

    /// <summary>
    /// The MASTERWARNINGFO switch has changed state.
    /// </summary>
    MASTERWARNINGFO,

    /// <summary>
    /// The SIDESTICKPRIORITYFO switch has changed state.
    /// </summary>
	SIDESTICKPRIORITYFO,

    /// <summary>
    /// The XBLEEDAUTO switch has changed state.
    /// </summary>
    XBLEEDAUTO,

    /// <summary>
    /// The XBLEEDOPEN switch has changed state.
    /// </summary>
    XBLEEDOPEN,

    /// <summary>
    /// The XBLEEDSHUT switch has changed state.
    /// </summary>
    XBLEEDSHUT,

    /// <summary>
    /// The APUAGENT switch has changed state.
    /// </summary>
    APUAGENT,

    /// <summary>
    /// The APUBLEED switch has changed state.
    /// </summary>
    APUBLEED,

    /// <summary>
    /// The APUFIREDOWN switch has changed state.
    /// </summary>
    APUFIREDOWN,

    /// <summary>
    /// The APUFIREUP switch has changed state.
    /// </summary>
    APUFIREUP,

    /// <summary>
    /// The APUFIRETEST switch has changed state.
    /// </summary>
    APUFIRETEST,

    /// <summary>
    /// The BLUEELECPUMP switch has changed state.
    /// </summary>
    BLUEELECPUMP,

    /// <summary>
    /// The YELLOWELECPUMP switch has changed state.
    /// </summary>
    YELLOWELECPUMP,

    /// <summary>
    /// The ENG1FIREDOWN switch has changed state.
    /// </summary>
    ENG1FIREDOWN,

    /// <summary>
    /// The ENG1FIREUP switch has changed state.
    /// </summary>
    ENG1FIREUP,

    /// <summary>
    /// The ENG2FIREDOWN switch has changed state.
    /// </summary>
    ENG2FIREDOWN,

    /// <summary>
    /// The ENG2FIREUP switch has changed state.
    /// </summary>
    ENG2FIREUP,

    /// <summary>
    /// The ENG1AGENT1 switch has changed state.
    /// </summary>
    ENG1AGENT1,

    /// <summary>
    /// The ENG1AGENT2 switch has changed state.
    /// </summary>
    ENG1AGENT2,

    /// <summary>
    /// The ENG1BLEED switch has changed state.
    /// </summary>
    ENG1BLEED,

    /// <summary>
    /// The ENG1GREENPUMP switch has changed state.
    /// </summary>
    ENG1GREENPUMP,

    /// <summary>
    /// The ENG1FIRETEST switch has changed state.
    /// </summary>
    ENG1FIRETEST,

    /// <summary>
    /// The ENG2AGENT1 switch has changed state.
    /// </summary>
    ENG2AGENT1,

    /// <summary>
    /// The ENG2AGENT2 switch has changed state.
    /// </summary>
    ENG2AGENT2,

    /// <summary>
    /// The ENG2BLEED switch has changed state.
    /// </summary>
    ENG2BLEED,

    /// <summary>
    /// The ENG2FIRE switch has changed state.
    /// </summary>
    ENG2FIRE,

    /// <summary>
    /// The ENG2YELLOWPUMP switch has changed state.
    /// </summary>
    ENG2YELLOWPUMP,

    /// <summary>
    /// The ENG2FIRETEST switch has changed state.
    /// </summary>
    ENG2FIRETEST,

    /// <summary>
    /// The ENG1MANSTART switch has changed state.
    /// </summary>
    ENG1MANSTART,

    /// <summary>
    /// The ENG2MANSTART switch has changed state.
    /// </summary>
    ENG2MANSTART,
    /// <summary>
    /// The . (MCDU) switch has changed state.
    /// </summary>
    PERIOD,
    /// <summary>
    /// The + (MCDU) switch has changed state.
    /// </summary>
    PLUS,
    /// <summary>
    /// The key 8 (MCDU) switch has changed state.
    /// </summary>
    KEY8,
    /// <summary>
    /// The key 9 (MCDU) switch has changed state.
    /// </summary>
    KEY9,
    /// <summary>
    /// The key A (MCDU) switch has changed state.
    /// </summary>
    KEYA,
    /// <summary>
    /// The key AIRPORT (MCDU) switch has changed state.
    /// </summary>
    AIRPORT,
    /// <summary>
    /// The arrow down (MCDU) switch has changed state.
    /// </summary>
    ARROWDOWN,
    /// <summary>
    /// The arrow left (MCDU) switch has changed state.
    /// </summary>
    ARROWLEFT,
    /// <summary>
    /// The arrow right (MCDU) switch has changed state.
    /// </summary>
    ARROWRIGHT,
    /// <summary>
    /// The arrow up (MCDU) switch has changed state.
    /// </summary>
    ARROWUP,
    /// <summary>
    /// The key ATCCOM (MCDU) switch has changed state.
    /// </summary>
    ATCCOMM,
    /// <summary>
    /// The key B (MCDU) switch has changed state.
    /// </summary>
    KEYB,
    /// <summary>
    /// The key BRT (MCDU) switch has changed state.
    /// </summary>
    BRT,
    /// <summary>
    /// The key C (MCDU) switch has changed state.
    /// </summary>
    KEYC,
    /// <summary>
    /// The key CLR (MCDU) switch has changed state.
    /// </summary>
    CLR,
    /// <summary>
    /// The key D (MCDU) switch has changed state.
    /// </summary>
    KEYD,
    /// <summary>
    /// The key DATA (MCDU) switch has changed state.
    /// </summary>
    DATA,
    /// <summary>
    /// The key DIM (MCDU) switch has changed state.
    /// </summary>
    DIM,
    /// <summary>
    /// The key DIR (MCDU) switch has changed state.
    /// </summary>
    DIR,
    /// <summary>
    /// The key E (MCDU) switch has changed state.
    /// </summary>
    KEYE,
    /// <summary>
    /// The key F (MCDU) switch has changed state.
    /// </summary>
    KEYF,
    /// <summary>
    /// The key FPLAN (MCDU) switch has changed state.
    /// </summary>
    FPLAN,
    /// <summary>
    /// The key FUELPRED (MCDU) switch has changed state.
    /// </summary>
    FUELPRED,
    /// <summary>
    /// The key G (MCDU) switch has changed state.
    /// </summary>
    KEYG,
    /// <summary>
    /// The key H (MCDU) switch has changed state.
    /// </summary>
    KEYH,
    /// <summary>
    /// The key I (MCDU) switch has changed state.
    /// </summary>
    KEYI,
    /// <summary>
    /// The key INIT (MCDU) switch has changed state.
    /// </summary>
    INIT,
    /// <summary>
    /// The key J (MCDU) switch has changed state.
    /// </summary>
    KEYJ,
    /// <summary>
    /// The key K (MCDU) switch has changed state.
    /// </summary>
    KEYK,
    /// <summary>
    /// The key L (MCDU) switch has changed state.
    /// </summary>
    KEYL,
    /// <summary>
    /// The LSK1 (MCDU) switch has changed state.
    /// </summary>
    LSK1,
    /// <summary>
    /// The LSK2 (MCDU) switch has changed state.
    /// </summary>
    LSK2,
    /// <summary>
    /// The LSK3 (MCDU) switch has changed state.
    /// </summary>
    LSK3,
    /// <summary>
    /// The LSK4 (MCDU) switch has changed state.
    /// </summary>
    LSK4,
    /// <summary>
    /// The LSK5 (MCDU) switch has changed state.
    /// </summary>
    LSK5,
    /// <summary>
    /// The LSK6 (MCDU) switch has changed state.
    /// </summary>
    LSK6,
    /// <summary>
    /// The key M (MCDU) switch has changed state.
    /// </summary>
    KEYM,
    /// <summary>
    /// The key MSCUMENU (MCDU) switch has changed state.
    /// </summary>
    MCDUMENU,
    /// <summary>
    /// The key N (MCDU) switch has changed state.
    /// </summary>
    KEYN,
    /// <summary>
    /// The key O (MCDU) switch has changed state.
    /// </summary>
    KEYO,
    /// <summary>
    /// The key OVFLY (MCDU) switch has changed state.
    /// </summary>
    OVFLY,
    /// <summary>
    /// The key P (MCDU) switch has changed state.
    /// </summary>
    KEYP,
    /// <summary>
    /// The key PERF (MCDU) switch has changed state.
    /// </summary>
    PERF,
    /// <summary>
    /// The key PROG (MCDU) switch has changed state.
    /// </summary>
    PROG,
    /// <summary>
    /// The key Q (MCDU) switch has changed state.
    /// </summary>
    KEYQ,
    /// <summary>
    /// The key R (MCDU) switch has changed state.
    /// </summary>
    KEYR,
    /// <summary>
    /// The RSK1 (MCDU) switch has changed state.
    /// </summary>
    RSK1,
    /// <summary>
    /// The RSK2 (MCDU) switch has changed state.
    /// </summary>
    RSK2,

    /// <summary>
    /// The RSK3 (MCDU) switch has changed state.
    /// </summary>
    RSK3,
    /// <summary>
    /// The RSK4 (MCDU) switch has changed state.
    /// </summary>
    RSK4,
    /// <summary>
    /// The RSK5 (MCDU) switch has changed state.
    /// </summary>
    RSK5,
    /// <summary>
    /// The RSK6 (MCDU) switch has changed state.
    /// </summary>
    RSK6,
    /// <summary>
    /// The key RADNAV (MCDU) switch has changed state.
    /// </summary>
    RADNAV,
    /// <summary>
    /// The key Z (MCDU) switch has changed state.
    /// </summary>
    KEYS,
    /// <summary>
    /// The key SECFPLAN (MCDU) switch has changed state.
    /// </summary>
    SECFPLAN,
    /// <summary>
    /// The key SLASH (MCDU) switch has changed state.
    /// </summary>
    SLASH,
    /// <summary>
    /// The key SPACE (MCDU) switch has changed state.
    /// </summary>
    SPACE,
    /// <summary>
    /// The key T (MCDU) switch has changed state.
    /// </summary>
    KEYT,
    /// <summary>
    /// The key U (MCDU) switch has changed state.
    /// </summary>
    KEYU,
    /// <summary>
    /// The key V (MCDU) switch has changed state.
    /// </summary>
    KEYV,
    /// <summary>
    /// The key W (MCDU) switch has changed state.
    /// </summary>
    KEYW,
    /// <summary>
    /// The key X (MCDU) switch has changed state.
    /// </summary>
    KEYX,
    /// <summary>
    /// The key Y (MCDU) switch has changed state.
    /// </summary>
    KEYY,

    /// <summary>
    /// The KEYZ (MCDU) switch has changed state.
    /// </summary>
    KEYZ,

    /// <summary>
    /// The ENG1N1MODE switch has changed state.
    /// </summary>
    ENG1N1MODE,

    /// <summary>
    /// The ENG2N1MODE switch has changed state.
    /// </summary>
    ENG2N1MODE,

    /// <summary>
    /// The POWER (REFUEL panel) switch has changed state.
    /// </summary>
    POWER,

    /// <summary>
    /// The REFUELING (REFUEL panel) switch has changed state.
    /// </summary>
    REFUELING,

    /// <summary>
    /// The DECREASE (REFUEL panel) switch has changed state.
    /// </summary>
    DECREASE,

    /// <summary>
    /// The INCREASE (REFUEL panel) switch has changed state.
    /// </summary>
    INCREASE,

    /// <summary>
    /// The FACELEFT (PUSHBACK panel) switch has changed state.
    /// </summary>
    FACELEFT,

    /// <summary>
    /// The STRAIGHTBACK (PUSHBACK panel) switch has changed state.
    /// </summary>
    STRAIGHTBACK,

    /// <summary>
    /// The FACERIGHT (PUSHBACK panel) switch has changed state.
    /// </summary>
    FACERIGHT,

    /// <summary>
    /// The PUSHBACKSTOP (PUSHBACK panel) switch has changed state.
    /// </summary>
    PUSHBACKSTOP,

    /// <summary>
    /// The PACK1 switch has changed state.
    /// </summary>
    PACK1,

    /// <summary>
    /// The PACK2 switch has changed state.
    /// </summary>
    PACK2,

    /// <summary>
    /// The PTU switch has changed state.
    /// </summary>
    PTU,

    /// <summary>
    /// The RAMAIR switch has changed state.
    /// </summary>
    RAMAIR,

    /// <summary>
    /// The RATMANON switch switch has changed state.
    /// </summary>
    RATMANON,

    /// <summary>
    /// The ALTPRESSURE (EFIS) encoder has changed value.
    /// </summary>
    ALTPRESSURE,

    /// <summary>
    /// The ALTITUDE (FCU) encoder has changed value.
    /// </summary>
    ALTITUDE,

    /// <summary>
    /// The HDG (FCU) encoder has changed value.
    /// </summary>
    HDG,

    /// <summary>
    /// The SPD (FCU) encoder has changed value.
    /// </summary>
    SPD,

    /// <summary>
    /// The VS (FCU) encoder has changed value.
    /// </summary>
    VS,

    /// <summary>
    /// The ISISQNH encoder has changed value
    /// </summary>
    ISISQNH,

    /// <summary>
    /// The kHz encoder has changed value.
    /// </summary>
    kHz,

    /// <summary>
    /// The MHz encoder has changed value.
    /// </summary>
    MHz,
    /// <summary>
    /// The ADF1VOLUME has changed state.
    /// </summary>
    ADF1VOLUME,
    /// <summary>
    /// The ADF2VOLUME has changed state.
    /// </summary>
    ADF2VOLUME,
    /// <summary>
    /// The CABVOLUME has changed state.
    /// </summary>
    CABVOLUME,
    /// <summary>
    /// The HF1VOLUME has changed state.
    /// </summary>
    HF1VOLUME,
    /// <summary>
    /// The HF2VOLUME has changed state.
    /// </summary>
    HF2VOLUME,
    /// <summary>
    /// The ILSVOLUME has changed state.
    /// </summary>
    ILSVOLUME,
    /// <summary>
    /// The INTVOLUME has changed state.
    /// </summary>
    INTVOLUME,
    /// <summary>
    /// The MKRVOLUME has changed state.
    /// </summary>
    MKRVOLUME,
    /// <summary>
    /// The MLSVOLUME has changed state.
    /// </summary>
    MLSVOLUME,
    /// <summary>
    /// The PAVOLUME has changed state.
    /// </summary>
    PAVOLUME,
    /// <summary>
    /// The VHF1VOLUME has changed state.
    /// </summary>
    VHF1VOLUME,
    /// <summary>
    /// The VHF2VOLUME has changed state.
    /// </summary>
    VHF2VOLUME,
    /// <summary>
    /// The VHF3VOLUME has changed state.
    /// </summary>
    VHF3VOLUME,
    /// <summary>
    /// The VOR1VOLUME has changed state.
    /// </summary>
    VOR1VOLUME,
    /// <summary>
    /// The VOR2VOLUME has changed state.
    /// </summary>
    VOR2VOLUME,

    /// <summary>
    /// The LANDINGELEVANALOG has changed state.
    /// </summary>
    LANDINGELEVANALOG,

    /// <summary>
    /// The LOWERDISPLAY has changed state.
    /// </summary>
    LOWERDISPLAY,

    /// <summary>
    /// The UPPERDISPLAY has changed state.
    /// </summary>
    UPPERDISPLAY,

    /// <summary>
    /// The CSNDBRIGHTNESS has changed state.
    /// </summary>
    CSNDBRIGHTNESS,

    /// <summary>
    /// The CSPFDBRIGHTNESS has changed state.
    /// </summary>
    CSPFDBRIGHTNESS,

    /// <summary>
    /// The FONDBRIGHTNESS has changed state.
    /// </summary>
    FONDBRIGHTNESS,

    /// <summary>
    /// The FOPFDBRIGHTNESS has changed state.
    /// </summary>
    FOPFDBRIGHTNESS,

    /// <summary>
    /// The FLAPS has changed state.
    /// </summary>
    FLAPS,

    /// <summary>
    /// The GRAVITYGEAR has changed state.
    /// </summary>
    GRAVITYGEAR,

    /// <summary>
    /// The SPEEDBRAKE has changed state.
    /// </summary>
    SPEEDBRAKE,

    /// <summary>
    /// The ELEVATORTRIMANALOG has changed state.
    /// </summary>
    ELEVATORTRIMANALOG,

    /// <summary>
    /// The ENG1LEVER has changed state.
    /// </summary>
    ENG1LEVER,

    /// <summary>
    /// The ENG2LEVER has changed state.
    /// </summary>
    ENG2LEVER,

    /// <summary>
    /// The AFTTEMPERATURE has changed state.
    /// </summary>
    AFTTEMPERATURE,

    /// <summary>
    /// The FWDTEMPERATURE has changed state.
    /// </summary>
    FWDTEMPERATURE,

    /// <summary>
    /// The AFTCARGOTEMPERATURE has changed state.
    /// </summary>
    AFTCARGOTEMPERATURE,

    /// <summary>
    /// The FWDCARGOTEMPERATURE has changed state.
    /// </summary>
    FWDCARGOTEMPERATURE,

    /// <summary>
    /// The COCKPITTEMPERATURE has changed state.
    /// </summary>
    COCKPITTEMPERATURE,
    /// <summary>
    /// The EMERCANCEL switch has changed state.
    /// </summary>
    EMERCANCEL,
    /// <summary>
    /// The PARKBRKOFF switch has changed state.
    /// </summary>
    PARKBRKOFF,
    /// <summary>
    /// The PARKBRKON switch has changed state.
    /// </summary>
    PARKBRKON,
    /// <summary>
    /// The ATC1 switch has changed state.
    /// </summary>
    ATC1,
    /// <summary>
    /// The ATC2 switch has changed state.
    /// </summary>
    ATC2,
    /// <summary>
    /// The ALTRPTGOFF switch has changed state.
    /// </summary>
    ALTRPTGOFF,
    /// <summary>
    /// The ALTRPTGON switch has changed state.
    /// </summary>
    ALTRPTGON,
    /// <summary>
    /// The RADARMAP switch has changed state.
    /// </summary>
    RADARMAP,
    /// <summary>
    /// The RADARSYSOFF switch has changed state.
    /// </summary>
    RADARSYSOFF,
    /// <summary>
    /// The RADARSYSON switch has changed state.
    /// </summary>
    RADARSYSON,
    /// <summary>
    /// The RADARWX switch has changed state.
    /// </summary>
    RADARWX,
    /// <summary>
    /// The RADARWXTURB switch has changed state.
    /// </summary>
    RADARWXTURB,
    /// <summary>
    /// The RADARTILT has changed state.
    /// </summary>
    RADARTILT,
    /// <summary>
    /// The RADARGAIN has changed state.
    /// </summary>
    RADARGAIN,
    /// <summary>
    /// The DOORVIDEO switch has changed state.
    /// </summary>
    DOORVIDEO,
    /// <summary>
    /// The EMEREXITLTOFF switch has changed state.
    /// </summary>
    EMEREXITLTOFF,

    /// <summary>
    /// The EMEREXITLTON switch has changed state.
    /// </summary>
    EMEREXITLTON,

    // Artificial events

    /// <summary>
    /// The VHF1 volume event. This is an artificial event based on hardware state.
    /// </summary>
    VHF1VOLUMEON,
    /// <summary>
    /// The VHF2 volume event. This is an artificial event based on hardware state.
    /// </summary>
    VHF2VOLUMEON,
    /// <summary>
    /// The VHF3 volume event. This is an artificial event based on hardware state.
    /// </summary>
    VHF3VOLUMEON,
    /// <summary>
    /// The HF1 volume event. This is an artificial event based on hardware state.
    /// </summary>
    HF1VOLUMEON,
    /// <summary>
    /// The HF2 volume event. This is an artificial event based on hardware state.
    /// </summary>
    HF2VOLUMEON,
    /// <summary>
    /// The INT volume event. This is an artificial event based on hardware state.
    /// </summary>
    INTVOLUMEON,
    /// <summary>
    /// The CAB volume event. This is an artificial event based on hardware state.
    /// </summary>
    CABVOLUMEON,
    /// <summary>
    /// The PA volume event. This is an artificial event based on hardware state.
    /// </summary>
    PAVOLUMEON,
    /// <summary>
    /// The VOR1 volume event. This is an artificial event based on hardware state.
    /// </summary>
    VOR1VOLUMEON,
    /// <summary>
    /// The VOR2 volume event. This is an artificial event based on hardware state.
    /// </summary>
    VOR2VOLUMEON,
    /// <summary>
    /// The MKR volume event. This is an artificial event based on hardware state.
    /// </summary>
    MKRVOLUMEON,
    /// <summary>
    /// The ILS volume event. This is an artificial event based on hardware state.
    /// </summary>
    ILSVOLUMEON,
    /// <summary>
    /// The ADF1 volume event. This is an artificial event based on hardware state.
    /// </summary>
    ADF1VOLUMEON,
    /// <summary>
    /// The ADF2 volume event. This is an artificial event based on hardware state.
    /// </summary>
    ADF2VOLUMEON,
    /// <summary>
    /// The STROBESLIGHTSAUTO event. This is an artificial event based on hardware state.
    /// </summary>
    STROBESLIGHTSAUTO,
    /// <summary>
    /// The LEFTLANDINGLIGHTOFF event. This is an artificial event based on hardware state.
    /// </summary>
    LEFTLANDINGLIGHTOFF,
    /// <summary>
    /// The RIGHTLANDINGLIGHTOFF event. This is an artificial event based on hardware state.
    /// </summary>
    RIGHTLANDINGLIGHTOFF,
    /// <summary>
    /// The NOSELIGHTTAXI event. This is an artificial event based on hardware state.
    /// </summary>
    NOSELIGHTTAXI,
    /// <summary>
    /// The SMOKINGSIGNAUTO event. This is an artificial event based on hardware state.
    /// </summary>
    SMOKINGSIGNAUTO,
    /// <summary>
    /// The SEATBELTSSIGNAUTO event. This is an artificial event based on hardware state.
    /// </summary>
    SEATBELTSSIGNAUTO,
    /// <summary>
    /// The EMEREXITLTARM event. This is an artificial event based on hardware state.
    /// </summary>
    EMEREXITLTARM,
    /// <summary>
    /// The ANNLTBRT event. This is an artificial event based on hardware state.
    /// </summary>
    ANNLTBRT,
    /// <summary>
    /// The DOMEDIM event. This is an artificial event based on hardware state.
    /// </summary>
    DOMEDIM,
    /// <summary>
    /// The ISISBAROPUSH event.
    /// </summary>
    ISISBAROPUSH,
    /// <summary>
    /// The ISISBAROPUSH event.
    /// </summary>
    ISISBRIGHTNESS,

    /// <summary>
    /// The MLSVOLUMEON event.
    /// </summary>
    MLSVOLUMEON,
};