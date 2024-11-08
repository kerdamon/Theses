using System;
using UnityEngine;
using System.IO;

namespace DefaultNamespace.SimulationControl
{
    public class FileLogger : MonoBehaviour
    {
        [SerializeField] private string logFileName;

        private StreamWriter _sr;
        private void Awake()
        {
            _sr = File.CreateText(logFileName);
        }

        public void LogLine(string line)
        {
            _sr.WriteLine(line);
        }

        private void OnApplicationQuit()
        {
            _sr.Close();
        }
    }
}