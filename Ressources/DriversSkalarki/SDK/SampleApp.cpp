// SampleApp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>
#include <vector>

#include "../../SkalarkiIOSDK/Src/EventClient.h"

static std::string GetPacketTypeName(PacketType packetType)
{
    switch (packetType)
    {
    case PacketType::ConnectionStateEvent:
        return "ConnectionStateEvent";
    case PacketType::HardwareEvent:
        return "HardwareStateEvent";
    }
}

static std::string GetDeviceTypeName(DeviceType deviceType)
{
    switch (deviceType)
    {
    case DeviceType::Glare:
        return "Glare";
    case DeviceType::Pedestal:
        return "Pedestal";
    case DeviceType::Overhead:
        return "Overhead";
    case DeviceType::MCDU1:
        return "MCDU1";
    case DeviceType::MCDU2:
        return "MCDU2";
    case DeviceType::MD80MCP:
        return "MD80MCP";
    case DeviceType::TQ:
        return "TQ";
    case DeviceType::Mip:
        return "Mip";
    case DeviceType::CLOCK:
        return "CLOCK";
    case DeviceType::ECAM:
        return "ECAM";
    case DeviceType::LOWERPED:
        return "LOWERPED";
    case DeviceType::ATC:
        return "ATC";
    case DeviceType::RMP1:
        return "RMP1";
    case DeviceType::RMP2:
        return "RMP2";
    case DeviceType::RMP3:
        return "RMP3";
    case DeviceType::ACP1:
        return "ACP1";
    case DeviceType::ACP2:
        return "ACP2";
    case DeviceType::ACP3:
        return "ACP3";
    case DeviceType::REFUEL:
        return "REFUEL";
    }
}

static void OnEvent(IOEvent* pEvent, void* pState)
{
    // Respond to hardware events... 
    std::cout << "Got " << GetDeviceTypeName(pEvent->Device) << " event " << pEvent->Event << " ";

    switch (pEvent->Source)
    {
    case HardwareSource::Switch:
        std::cout << (pEvent->Value ? "pressed" : "released");
        break;
    case HardwareSource::Axis:
        std::cout << pEvent->Value << std::endl;
        break;
    case HardwareSource::Encoder:
        std::cout << (pEvent->Value ? "clockwise" : "counter clockwise");
    }

    std::cout << std::endl;

    // Receiver is responsible for deleting the event
    delete pEvent;
}

static void OnDeviceChange(HardwareConnectionStateEvent* pEvent, void* pState)
{
    std::cout << "Got " << GetDeviceTypeName(pEvent->Device) << (pEvent->Connected ? " connected" : " disconnected") << std::endl;
    delete pEvent;
}

int main()
{
    // Pass in here what will be passed to your callback on invocation
    int stateVar = NULL;
    EventClient* pClient = EventClient::Create("127.0.0.1", 53000, OnEvent, OnDeviceChange, &stateVar);
    std::cout << "Press a key to connect" << std::endl;
    std::cin.ignore();

    pClient->Connect();

    std::cout << "Press any key to register events" << std::endl;

    auto s = vector<IEventDescriptor*>();
    s.push_back(Switches::TQ::ATHR1OFF);
    s.push_back(Switches::TQ::ATHR2OFF);

    std::cin.ignore();

    pClient->RegisterEvents(s, true);

    std::cin.ignore();

    pClient->Disconnect();

    return 0;
}

