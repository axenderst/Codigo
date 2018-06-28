﻿using System;
using System.Collections.Generic;
using SECI.FactoryData.Repositories;
using SECI.Entities;

namespace SECI.Business
{
    public class FacadeMapa
    {

        //Inserta mapas a la bd
        public static int InsertaMapa(Mapas mapa, EncabezadoMapas encabezadoMapa)
        {
            try
            {
                return new RepositorioConsultas().InsertaMapa(mapa, encabezadoMapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Se genera la lista de mapas existentes
        public static List<Mapas> ObtenerMapas()
        {
            try
            {
                return new RepositorioConsultas().mostrarMapas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static EncabezadoMapas ConsultaEncabezadoByllmapa(int llmapa)
        {
            try
            {
                return new RepositorioMapas().ConsultaEncabezadoByllmapa(llmapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //obtiene los datos del mapa mediante clavemapa
        public static List<Mapas> ObtenerMapasByPrefijo(string ClaveMapa)
        {
            try
            {
                return new RepositorioConsultas().ConsultaMapasByPrefijo(ClaveMapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //obtiene los datos del mapa mediante IdMapa
        public static Mapas ObtenerMapasById(int IdMapa)
        {
            try
            {
                return new RepositorioConsultas().ObtenerMapasById(IdMapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Se genera la busqueda del mapa mediante clavemapa
        public static List<Mapas> BuscaMapa(string ClaveMapa)
        {
            try
            {
                return new RepositorioConsultas().BuscaMps(ClaveMapa);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        //Verifica una busqueda de mapa sin importar carcateres ni mayus-minus
        public static List<Mapas> VerificarMapa (string ClaveMapa)
        {
            try
            {
                return new RepositorioConsultas().examinaMapa(ClaveMapa);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        //Verifica una busqueda de mapa sin importar carcateres ni mayus-minus
        public static int modificaMapa(Mapas mapaId, EncabezadoMapas encabezadoMap)
        {
            try
            {
                return new RepositorioConsultas().updateMapas(mapaId, encabezadoMap);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void BorraMapaByIdMapa(int idMapa)
        {
            try
            {
                new RepositorioMapas().BorraMapaByIdMapa(idMapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}

