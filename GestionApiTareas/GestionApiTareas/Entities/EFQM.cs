using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class EFQM
    {

        [Key]
        public long idEFQM { get; set; }
        public int Leadership { get; set; }
        public int Strategy { get; set; }
        public int People { get; set; }
        public int Partnerships { get; set; }
        public int Processes { get; set; }
        public int ResultsCustomer { get; set; }
        public int ResultsPeople { get; set; }
        public int ResultsSociety { get; set; }
        public int ResultsKey { get; set; }

        public double CalculateScore()
        {
            double enablersScore = (Leadership + Strategy + People + Partnerships + Processes) / 5.0;
            double resultsScore = (ResultsCustomer + ResultsPeople + ResultsSociety + ResultsKey) / 4.0;
            double totalScore = (enablersScore * 0.5) + (resultsScore * 0.5);
            return totalScore;
        }

        public string Evaluate()
        {
            double score = CalculateScore();
            if (score > 80)
            {
                return "Excelente";
            }
            else if (score > 60)
            {
                return "Muy Bueno";
            }
            else if (score > 40)
            {
                return "Bueno";
            }
            else if (score > 20)
            {
                return "Aceptable";
            }
            else
            {
                return "Insuficiente";
            }
        }
    }
}
