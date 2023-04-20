namespace System.Device.I2c;

public static class I2cDeviceExtensions
{
    public static void WriteCommand(this I2cDevice i2cDevice, byte command, 
        Func<byte, short>? delay = null)
    {
        i2cDevice.WriteByte(command);
        Thread.Sleep(delay?.Invoke(command) ?? 0);
    }

    public static byte ReadByte(this I2cDevice i2cDevice, byte command, byte address, 
        Func<byte, short>? delay = null)
    {
        i2cDevice.Write(new byte[] { command, address });
        Thread.Sleep(delay?.Invoke(command) ?? 0);
        return i2cDevice.ReadByte();
    }

    public static byte ReadByte(this I2cDevice i2cDevice, byte address, 
        Func<byte, short>? delay = null)
    {
        i2cDevice.Write(new byte[] { address });
        Thread.Sleep(delay?.Invoke(address) ?? 0);
        return i2cDevice.ReadByte();
    }

    public static void WriteByte(this I2cDevice i2cDevice, byte command, byte address, byte value, 
        Func<byte, short>? delay = null)
    {
        i2cDevice.Write(new byte[] { command, address, value });
        Thread.Sleep(delay?.Invoke(command) ?? 0);
    }

    public static void WriteByte(this I2cDevice i2cDevice, byte address, byte value, 
        Func<byte, short>? delay = null)
    {
        i2cDevice.Write(new byte[] { address, value });
        Thread.Sleep(delay?.Invoke(address) ?? 0);
    }

    public static ushort ReadUShort(this I2cDevice i2cDevice)
    {
        Span<byte> buf = stackalloc byte[2];
        i2cDevice.Read(buf);

        return (ushort)(buf[0] << 8 | buf[1]);
    }
    public static ushort ReadUShort(this I2cDevice i2cDevice, byte address)
    {
        i2cDevice.WriteCommand(address, x => 0);
        return i2cDevice.ReadUShort();
    }

    public static short ReadShort(this I2cDevice i2cDevice)
    {
        Span<byte> buf = stackalloc byte[2];
        i2cDevice.Read(buf);

        return (short)(buf[0] << 8 | buf[1]);
    }
    public static short ReadShort(this I2cDevice i2cDevice, byte address)
    {
        i2cDevice.WriteCommand(address, x => 0);
        return i2cDevice.ReadShort();
    }
}