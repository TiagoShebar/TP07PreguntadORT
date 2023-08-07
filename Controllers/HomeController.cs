using Microsoft.AspNetCore.Mvc;

namespace TP07PreguntadORT.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego(){
        Juego.InicializarJuego();
        ViewBag.Categorias = Juego.ObtenerCategorias();
        ViewBag.Dificultades = Juego.ObtenerDificultades();
        return View();
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria){
        if(Juego.CargarPartida(username, dificultad, categoria)){
            return RedirecToAction("Jugar");
        }
        else{
            return RedirecToAction("ConfigurarJuego");
        }
    }

    public IActionResult Jugar(){
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        if(Preguntas != null){
            return View("Fin");
        }
        else{
            ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.idPregunta);
            return View("Juego");
        }
    }

    [HttpPost] public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
        ViewBag.Correcta = Juego.VerificarRespuesta(idPregunta, idRespuesta);
        int i = 0;
        while(i<ViewBag.Respuestas && ViewBag.Respuestas[i].IdPregunta == idPregunta && !ViewBag.Respuestas[i].Correcta){
            i++;
        }
        ViewBag.RespuestaCorrecta = ViewBag.Respuestas[i];
        return View("Respuesta");
    }
}
