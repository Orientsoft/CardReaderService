using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderService
{
    class WaterCardReader
    {
        // DLL imports
        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "ReadGasCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int ReadGasCard(
            short ifacid,
            short com,
            Int32 baud,
            ref short klx,
            ref short kzt,
            byte[] kh,
            byte[] tm,
            ref Int32 ql,
            ref Int32 cs,
            ref Int32 ljgql,
            ref Int32 bkcs,
            ref Int32 ljyql,
            ref Int32 syql
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "WriteNewCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int WriteNewCard(
            short com,
            Int32 baud,
            short klx,
            short kzt,
            byte[] kh,
            byte[] tm,
            Int32 ql,
            short cs,
            Int32 ljgql,
            short bkcs,
            Int32 ljyql,
            Int32 metertype,
            Int32 Alarmnum,
            Int32 ihoard,
            Int32 metermode,
            Int32 paramver,
            byte[] pricestarttime,
            byte[] stepinfos
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "WriteGasCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int WriteGasCard(
            short com,
            Int32 baud,
            short klx,
            byte[] kh,
            short ql,
            short cs,
            Int32 ljgql,
            Int32 bkcs,
            Int32 metertype,
            Int32 Alarmnum,
            Int32 ihoard,
            Int32 metermode,
            Int32 paramver,
            byte[] pricestarttime,
            byte[] stepinfos
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "ClearMeterCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int ClearMeterCard(
            int imetertype
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "CheckMeterCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int CheckMeterCard(
            int imetertype
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "DatetimeMeterCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int DatetimeMeterCard(
            int imetertype,
            byte[] datetimes
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "ParamsMeterCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int ParamsMeterCard(
            int imetertype,
            int Alarmnum,
            int ihoard
        );

        [DllImportAttribute(
            "gsIneterface.dll",
            EntryPoint = "ModeMeterCard",
            CallingConvention = CallingConvention.StdCall
        )]
        public static extern int ModeMeterCard(
            int imetertype,
            int iMode,
            float Price1,
            float Price2,
            float Price3,
            float Price4,
            int StepMount1,
            int StepMount2,
            int StepMount3
        );
    }
}
