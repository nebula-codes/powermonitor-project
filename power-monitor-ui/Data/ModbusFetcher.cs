using EasyModbus;

namespace power_monitor_ui.Data
{
    public class ModbusFetcher
    {

        public ModbusClient Client { get; set; }
        string IP { get; set; }
        int Port { get; set; }

        public ModbusFetcher(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
            Client = new ModbusClient(this.IP, this.Port);
        }

        void connectModbus()
        {
            Client.UnitIdentifier = Convert.ToByte(1);
            Client.ConnectionTimeout = 1500;
            Client.Parity = System.IO.Ports.Parity.None;
            Client.Connect();
        }

        void discModbus()
        {
            Client.Disconnect();
        }


        public async Task<float> getModBusInfoSize2(int registerValue)
        {
            if (Client.Connected)
            {
                int[] values = Client.ReadHoldingRegisters(registerValue, 2);
                values[1] = values[1] + 32767;

                string firstVal = values[0].ToString("X");
                string secondVal = (values[1]).ToString("X");

                if (firstVal.Length != 4)
                {
                    if (firstVal.Length > 4)
                    {
                        firstVal = firstVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - firstVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            firstVal = firstVal + "0";
                        }
                    }

                }

                if (secondVal.Length != 4)
                {
                    if (secondVal.Length > 4)
                    {
                        secondVal = secondVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - secondVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            secondVal = secondVal + "0";
                        }
                    }
                }

                string hexString = firstVal + secondVal;

                int num = int.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);
                byte[] floatVals = BitConverter.GetBytes(num);
                float f = BitConverter.ToSingle(floatVals, 0);

                return f;
            }
            else
            {
                connectModbus();
                await getModBusInfoSize2(registerValue);
            }
            discModbus();
            return 0;

        }

        public async Task<int> getModBusInfoSize4(int registerValue)
        {
            int value = 69;
            if (Client.Connected)
            {
                int[] values = Client.ReadHoldingRegisters(registerValue - 1, 4);
                string firstVal = values[0].ToString("X");
                string secondVal = (values[1]).ToString("X");
                string thirdVal = (values[2]).ToString("X");
                string fourthVal = (values[3]).ToString("X");

                if (firstVal.Length != 4)
                {
                    if (firstVal.Length > 4)
                    {
                        firstVal = firstVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - firstVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            firstVal = "0" + firstVal;
                        }
                    }

                }

                if (secondVal.Length != 4)
                {
                    if (secondVal.Length > 4)
                    {
                        secondVal = secondVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - secondVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            secondVal = "0" + secondVal;
                        }
                    }
                }

                if (thirdVal.Length != 4)
                {
                    if (thirdVal.Length > 4)
                    {
                        thirdVal = thirdVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - thirdVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            thirdVal = "0" + thirdVal;
                        }
                    }
                }

                if (fourthVal.Length != 4)
                {
                    if (fourthVal.Length > 4)
                    {
                        fourthVal = fourthVal.Substring(4, 4);
                    }
                    else
                    {
                        var missingAmout = 4 - fourthVal.Length;

                        for (int i = 0; i < missingAmout; i++)
                        {
                            fourthVal = "0" + fourthVal;
                        }
                    }
                }

                string hexString = firstVal + secondVal + thirdVal + fourthVal;
                value = int.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

                return value;
            }
            else
            {

                connectModbus();
                return await getModBusInfoSize4(registerValue);
            }
            discModbus();
            return value;

        }
    }

}
