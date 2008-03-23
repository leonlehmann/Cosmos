﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Hardware;
using Cosmos.Hardware.PC.Bus;

namespace Cosmos.Hardware.Network.Devices.RTL8139.Register
{
    /// <summary>
    /// Receive Configuration Register is used to set receive configuration.
    /// Offset 44h from main memory.
    /// </summary>
    public class ReceiveConfigurationRegister
    {
        private PCIDevice pci;
        private UInt32 rcrAddress;

        public static ReceiveConfigurationRegister Load(PCIDevice pcicard)
        {
            UInt32 address = pcicard.BaseAddress1 + (byte)MainRegister.Bit.RxConfig;
            return new ReceiveConfigurationRegister(pcicard, address);
        }

        public ReceiveConfigurationRegister(PCIDevice hw, UInt32 adr)
        {
            pci = hw;
            rcrAddress = adr;
        }

        public void Init()
        {
            UInt32 data = (UInt32)(BitValue.RBLEN0 | BitValue.MXDMA0 | BitValue.MXDMA1 | BitValue.AB | BitValue.AM | BitValue.APM);
            Console.WriteLine("Data in INIT for RX is: " + data);
            IOSpace.Write32(rcrAddress, data);
        }

        public UInt32 RCR 
        { 
            get 
            { 
                return IOSpace.Read32(rcrAddress);
            } 
            private set { ;} 
        }

        public enum BitValue : uint
        {
            /// <summary>
            /// Accept Physical Address Packets. 0 rejects, 1 accepts.
            /// </summary>
            AAP = BinaryHelper.BitPos.BIT0,
            /// <summary>
            /// Accept Physical Match Packets. 0 rejects, 1 accepts.
            /// </summary>
            APM = BinaryHelper.BitPos.BIT1,
            /// <summary>
            /// Accept Multicast Packets. 0 rejects, 1 accepts.
            /// </summary>
            AM = BinaryHelper.BitPos.BIT2,
            /// <summary>
            /// Accept Broadcast Packets. 0 rejects, 1 accepts.
            /// </summary>
            AB = BinaryHelper.BitPos.BIT3,
            /// <summary>
            /// Accept Runt Packets (packets smaller than 64 bytes - but over 8 bytes.)
            /// </summary>
            AR = BinaryHelper.BitPos.BIT4,
            /// <summary>
            /// Accept Error Packets (Packets with CRC error, alignment error and/or collided fragments).
            /// </summary>
            AER = BinaryHelper.BitPos.BIT5,
            /// <summary>
            /// EEPROM used. 0 = 9346, 1 = 9356.
            /// </summary>
            EEPROM = BinaryHelper.BitPos.BIT6,
            /// <summary>
            /// (Only C mode) 0: Wrap incoming packet to beginning of next RxBuffer.
            /// 1: Overflow packet even after coming to end of buffer.
            /// </summary>
            WRAP = BinaryHelper.BitPos.BIT7,
            /// <summary>
            /// Three bits wide.
            /// Max DMA Burst Size per Rx DMA Burst. 010 = 64 bytes, 011 = 128 bytes, 100 = 256 bytes.
            /// </summary>
            MXDMA0 = BinaryHelper.BitPos.BIT8,
            MXDMA1 = BinaryHelper.BitPos.BIT9,
            MXDMA2 = BinaryHelper.BitPos.BIT10,
            /// <summary>
            /// RxBuffer Length.
            /// 00 = 8k + 16 byte
            /// 01 = 16k + 16 byte
            /// 10 = 32k + 16 byte
            /// 11 = 64k + 16 byte
            /// </summary>
            RBLEN0 = BinaryHelper.BitPos.BIT11,
            RBLEN1 = BinaryHelper.BitPos.BIT12,
            /// <summary>
            /// Rx FIFO Threshold. Three bits wide.
            /// When recieved byte count matches this level the incoming data will
            /// be transferred from FIFO to host memory.
            /// See 8139C+ specs for valid values.
            /// </summary>
            RXFTH0 = BinaryHelper.BitPos.BIT13,
            /// <summary>
            /// Receive Error Packets Larger than 8 bytes. Yes if 1. If 0 (default) then 
            /// 64-byte error packets are received. Also depends on AER or AR bits.
            /// </summary>
            RER8 = BinaryHelper.BitPos.BIT16,
            /// <summary>
            /// Multiple Early Interrupt Select. 1 bit wide.
            /// </summary>
            MULERINT = BinaryHelper.BitPos.BIT17,
            /// <summary>
            /// Early Rx Threshold. 4 bits wide.
            /// </summary>
            ERTH0 = BinaryHelper.BitPos.BIT24
        }
    }
}
