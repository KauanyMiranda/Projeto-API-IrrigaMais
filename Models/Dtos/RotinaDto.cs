namespace Projeto_IrrigaMais_API.Models.Dtos
{
    public class RotinaDto
    {
        public string tipo_execucao { get; set; }
        public DateTime horario { get; set; }
        public int frequencia { get; set; }
        public string nome_rotina { get; set; }

        public bool dia_seg { get; set; }
        public bool dia_ter { get; set; }
        public bool dia_qua { get; set; }
        public bool dia_qui { get; set; }
        public bool dia_sex { get; set; }
        public bool dia_sab { get; set; }
        public bool dia_dom { get; set; }
    }
}
