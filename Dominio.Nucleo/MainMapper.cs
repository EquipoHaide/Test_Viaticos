using System;
using System.Collections.Generic;
using AutoMapper;

namespace Dominio.Nucleo
{
    public class MainMapper
    {
        private static MainMapper instance;
        public static MainMapper Instance
        {
            get
            {
                return instance;
            }
        }
        public static bool Inicializando { get; set; }

        public MainMapper()
        {
            Inicializando = true;
        }

        public IMapper Mapper { get; private set; }
        private Dictionary<string, ProfileMapper> Profiles { get; set; }
        public bool EsInicializado { get; set; }

        public void Inicializar()
        {
            if (MainMapper.Instance != null && MainMapper.Instance.EsInicializado) return;

            Mapper = new MapperConfiguration(cfg =>
            {
                if (Profiles != null)
                {
                    foreach (var profile in Profiles)
                    {
                        cfg.AddProfile(profile.Value);
                    }
                }
            }).CreateMapper();
            MainMapper.instance = this;
            MainMapper.Inicializando = false;
            EsInicializado = true;
        }

        public ProfileMapper GetProfile(string profileName)
        {
            if (MainMapper.Instance != null && MainMapper.Instance.EsInicializado) return null;

            if (Profiles == null)
                Profiles = new Dictionary<string, ProfileMapper>();

            if (string.IsNullOrEmpty(profileName) || string.IsNullOrWhiteSpace(profileName)) return null;

            if (Profiles.ContainsKey(profileName)) return null;

            var profile = new ProfileMapper();
            try
            {
                Profiles.Add(profileName, profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return profile;
        }


    }
}
