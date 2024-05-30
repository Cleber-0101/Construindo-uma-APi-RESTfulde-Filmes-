//using System.ComponentModel.DataAnnotations;

//namespace FilmesAPI.Data.Dtos
//{
//    public class CreateFilmeDto
//    {
//        //parametros obrigatorios 
//        [Required(ErrorMessage = "O titulo do filme é obrigatorio")]
//        public string Titulo { get; set; }

//        [Required(ErrorMessage = "o genero é obrigatorio")]
//        [StringLength(15, ErrorMessage = "O tamanho do genero não pode exceder os 15 caracteres")]
//        public string Genero { get; set; }

//        [Required(ErrorMessage = "A duração deve ser preenchida")]
//        [Range(70, 600, ErrorMessage = "A duração deve ser entre 70 e  600 minutos")]
//        public int Duracao { get; set; }
//    }
//}
