namespace ThanaNita.MonoGameTnt
{
    // ค่า config ควร set ตอน init game และไม่ควรเปลี่ยนแปลงกลางคัน
    // เพราะบางคลาสอาจ caching ค่าไว้ใช้ เช่นคลาส DirectionKey
    public static class GlobalConfig
    {
        public static bool GeometricalYAxis { get; set; } = false;
    }
}
