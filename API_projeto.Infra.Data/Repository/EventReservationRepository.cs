using Dapper;
using MySqlConnector;
using System.Globalization;
using API_projeto.Service.Interface;
namespace API_projeto.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private string _stringConnection { get; set; }
        //Construtor
        public EventReservationRepository() {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");

        }
        //Inclusão de uma nova reserva;
        public async Task<bool> InserirReserva(EventReservationEntity eventReservation)
        {
            string query = "INSERT INTO EventReservation(idEvent,personName,quantity) VALUES(@idEvent,@personName,@quantity)";
            DynamicParameters parametros = new(eventReservation);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }
        //Edição da quantidade de uma reserva; *Autenticação e Autorização admin
        public async Task<bool> EditarQuantidadeReserva(int numero, long idReservation)
        {
            string query  = "UPDATE EventReservation  set quantity = @numero where idReservation=@id";
            DynamicParameters parametros = new();
            parametros.Add("numero", numero);
            parametros.Add("id", idReservation);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }
        //Consulta de reserva pelo PersonName e Title do evento, utilizando similaridade para o title; *Autenticação
        
        public async Task<List<EventReservationEntity>> ConsultaPersonTitle(string nome , string tituloEvento)
        {
            string query = "SELECT * FROM CityEvent INNER JOIN EventReservation ON CityEvent.IdEvent = EventReservation.IdEvent WHERE PersonName = @nome AND Title LIKE @tituloEvento;";
            tituloEvento = $"%{tituloEvento}%";
            DynamicParameters parameters = new();
            parameters.Add("nome", nome);
            parameters.Add("tituloEvento", tituloEvento);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            return  ( await conn.QueryAsync<EventReservationEntity>(query, parameters)).ToList();

        }
        //Remoção de uma reserva; *Autenticação e Autorização admin
        public async Task<bool> Deletar(long id)
        {
            string query = "DELETE FROM EventReservation where idReservation = @id";
            DynamicParameters parameters = new();
            parameters.Add("id", id);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas =  conn.Execute (query, parameters);
            return linhasAfetadas > 0;
        }
    }
}
