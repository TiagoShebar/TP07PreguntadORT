using System.Data.SqlClient;
using Dapper;

static class BD
{

    private static string _connectionString = @"Server=localhost;DataBase=NombreBase;Trusted_Connection=True;";

    public static List<Categoria> ObtenerCategorias(){
        string SQL = "SELECT * FROM Categorias";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            List<Categoria> _ListadoCategorias = db.Query<Categoria>(SQL).ToList();
        }
        return _ListadoCategorias;
    }

    public static List<Dificultad> ObtenerDificultades()(){
        string SQL = "SELECT * FROM Dificultades";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            List<Categoria> _ListadoDificultades = db.Query<Dificultad>(SQL).ToList();
        }
        return _ListadoDificultades;
    }
    
    public static List<Pregunta> ObtenerPreguntas(int dificultad, int categoria){
        string SQL;
        List<Pregunta> _ListadoPreguntas;
        using(SqlConnection db = new SqlConnection(_connectionString)){
            if(dificultad == -1 && categoria == -1){
                SQL = "SELECT * FROM Preguntas";
                _ListadoPreguntas = db.Query<Pregunta>(SQL).ToList();
            }
            else if(dificultad == -1 && categoria != -1){
                SQL = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria";
                _ListadoPreguntas = db.Query<Pregunta>(SQL, new { pIdCategoria = categoria}).ToList();
            }
            else if(categoria == -1 && dificultad != -1){
                SQL = "SELECT * FROM Preguntas WHERE IdDificultad = @pIdDificultad";
                _ListadoPreguntas = db.Query<Pregunta>(SQL, new { pIdDificultad = dificultad}).ToList();
            }
            else{
                SQL = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria and IdDificultad = @pIdDificultad";
                _ListadoPreguntas = db.Query<Pregunta>(SQL, new { pIdCategoria = categoria, pIdDificultad = dificultad}).ToList();
            }
        }

        return _ListadoPreguntas;
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> preguntas){
        string SQL = "SELECT * FROM Respuestas WHERE IdPregunta = @pIdPregunta";
        List<Respuesta> _ListadoRespuestas = new List<Respuesta>();
        using(SqlConnection db = new SqlConnection(_connectionString)){
            foreach (Pregunta pregunta in preguntas)
            {
                List<Respuesta> respuestas = db.Query<Respuesta>(SQL, new { pIdPregunta = pregunta.IdPregunta }).ToList();
                _ListadoRespuestas.AddRange(respuestas);
            }
        } 
        return _ListadoRespuestas;
    }

}