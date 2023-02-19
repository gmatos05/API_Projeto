using System.Diagnostics.Eventing.Reader;
using API_projeto.Service.Interface;
using Dapper;
using MySqlConnector;

namespace API_projeto.Repository
{
    public class CityEventRepository:ICityEventRepository
    {
        
        private string _stringConnection { get; set; }
        public CityEventRepository() {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }
        //Inclusão de um novo evento; *Autenticação e Autorização admin
        public async Task<bool> InserirEvento(CityEventEntity cityevent) {
            string query = "INSERT INTO CityEvent(Title,description,dateHourEvent,local,address,price,status) VALUES(@Title,@description,@dateHourEvent,@local,@address,@price,@status)";
            DynamicParameters parametros = new(cityevent) ;
           
           
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query,parametros) ;
            
            return linhasAfetadas > 0;
        }
        //Edição de um evento existente, filtrando por id; *Autenticação e Autorização admin é o mesmo que fazer um update
        public async Task<bool> EditarEvento(CityEventEntity cityevent,int id ) {
            string query = "UPDATE CityEvent  set Title = @title,description = @description,dateHourEvent = @dateHourEvent,local = @local,address = @address,price = @price,status = @status where idEvent=@id";
            
            var parametros = new DynamicParameters(new
            {
                cityevent.Title,
                cityevent.Description,
                cityevent.DateHourEvent,
                cityevent.Local,
                cityevent.Address,
                cityevent.Price,
                cityevent.Status
            });
            parametros.Add("id", id);

            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }
        //Remoção de um evento, caso o mesmo não possua reservas em andamento, caso possua inative-o; *Autenticação e Autorização admin
        public async Task<bool> ConsultaReservas(int idEvento)
        {
            string query = "Select * from EventReservation WHERE idEvent = @id";
            DynamicParameters parametros = new();
            parametros.Add("id", idEvento);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            return  conn.QueryFirstOrDefault(query, parametros) == null;

        }
        public async Task<bool> Inativa(int idEvento)
        {
            string query = "UPDATE CityEvent set status = false WHERE idEvent = @id";
            DynamicParameters parametros = new();
            parametros.Add("id", idEvento);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhaAfetadas = conn.Execute(query, parametros);
            return linhaAfetadas > 0;

        }
        public async Task<bool> ExcluirEvento(int id)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent =@id";
            DynamicParameters parametros = new();
            parametros.Add("id", id);
            using MySqlConnection conn  = new MySqlConnection(_stringConnection);
            int linhaAfetadas = conn.Execute(query, parametros);
            return linhaAfetadas > 0; 
        }
       
            
        //Consulta por título, utilizando similaridades, por exemplo, caso pesquise Show, traga todos os eventos que possuem a palavra Show no título;
        public async Task<List<CityEventEntity>> ConsultaTitulo(string nome)
        {
            var query = "SELECT * FROM CityEvent WHERE Title like @nome";
            nome = $"%{nome}%";


            var parameters = new DynamicParameters(nome);

            parameters.Add("nome", nome);

            using MySqlConnection conn = new(_stringConnection);
            try
            {
                return ( conn.Query<CityEventEntity>(query, parameters)).ToList();

            }
            catch
            {
                return null;
            }
        
            //return(await conn.QueryAsync<CityEventEntity>(query, parameters)).ToList();
                
        

        }
        //Consulta por local e data;
        public async  Task<List<CityEventEntity>> ConsultaLocalData(string local, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE Local = @local AND Date(dateHourEvent) = @data;";
            
            var parameters = new DynamicParameters();
            parameters.Add("local", local);
            parameters.Add("data", data);
            using MySqlConnection conn = new(_stringConnection);
            //return (await conn.QueryAsync<CityEventEntity>(query, parameters)).ToList();
            return (conn.Query<CityEventEntity>(query,parameters)).ToList();
        }
        //Consulta por range de preço e a data;
        public async Task<List<CityEventEntity>> ConsultaPrecoData(decimal minPrice, decimal maxPrice, DateTime data)
        {
            string query = "SELECT * FROM CityEvent WHERE Price BETWEEN @minPrice AND @maxPrice AND DATE(dateHourEvent) = @date;";
            var parameters = new DynamicParameters();
            parameters.Add("minPrice", minPrice);
            parameters.Add("maxPrice", maxPrice);
            parameters.Add("date",data);
            using MySqlConnection conn = new(_stringConnection);
            return ( conn.Query<CityEventEntity>(query, parameters)).ToList();
           

        }
    }
}
