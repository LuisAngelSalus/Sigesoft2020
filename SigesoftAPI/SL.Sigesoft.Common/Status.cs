namespace SL.Sigesoft.Models.Enum
{
    public enum YesNo
    {
        Yes =1,
        No =0
    }

    public enum RecordType
    {     
        Temporal = 1,     
        NoTemporal = 2
    }
    public enum RecordStatus
    {
        Grabado = 0,
        Modificado = 1,
        Agregado = 2,
        EliminadoLogico = 3
    }

    public enum StatusQuotation
    {
        Seguimiento = 1,
        Aceptada = 2,
        Descartada = 3,
        Potencial = 4
    }

    public enum TypeFormat
    {
        RM312 = 1,
        Anexo16 = 2,
        Anexo16A= 3,
        Ambos = 4,
        Componentes = 5
    }

    public enum TypeDocument
    {
        DNI =1,
        Pasaporte =2,
        PTP=3
    }
}
