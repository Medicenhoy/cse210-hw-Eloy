using System;

namespace Resumes
{
    public class Job
    {
        // Membros da classe Job
        public string _jobTitle;
        public string _company;
        public int _startYear;
        public int _endYear;

        // Método para exibir os detalhes do trabalho
        public void DisplayJobDetails()
        {
            Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
        }
    }
}
