static class Juego{
    static private string _username;
    static private int _puntajeActual;
    static private int _cantidadPreguntasCorrectas;
    static private List<Pregunta> _preguntas;
    static private List<Respuesta> _respuestas;

    public void InicializarJuego(){
        _username = null;
        _puntajeActual = 0;
        _cantidadPreguntasCorrectas = 0;
    }

    public List<Categoria> ObtenerCategorias(){
        return BD.ObtenerCategorias();
    }

    public List<Dificultad> ObtenerDificultades(){
        return BD.ObtenerDificultades();
    }

    public bool CargarPartida(string username, int dificultad, int categoria)(){
        _preguntas = BD.ObtenerPreguntas();
        _respuestas = BD.ObtenerRespuestas(_preguntas);
        if(_preguntas != null){
            return true;
        }
        else{
            return false;
        }
    }

    public Pregunta ObtenerProximaPregunta(){
        if(_preguntas.Count == 0){
            return null;
        }
        else{
            Random r = new Random();
            return _preguntas[r.next(_preguntas[0],_preguntas.Count());];
        }
    }

    public List<Respuesta> ObtenerProximasRespuestas(int idPregunta){
        List<Respuesta> respuestas = new List<Respuesta>();
        foreach (Respuesta r in _respuestas)
        {
            if(r.IdPregunta == idPregunta){
                respuestas.Add(r);
            }
        }
        return respuestas;
    }

    public bool VerificarRespuesta(int idPregunta, int idRespuesta){
        int i = 0;
        while(i<_respuestas.Count() && _respuestas[i].IdRespuesta != idRespuesta){
            i++;
        }
        return _respuestas[i].Correcta;
        if(_respuestas[i].Correcta){
            _puntajeActual + 10;
            _cantidadPreguntasCorrectas++;
        }
        i = 0;
        while(i<_preguntas.Count() && _preguntas[i].IdPregunta != idPregunta){
            i++;
        }
        _preguntas.RemoveAt[i];
        
    }

}