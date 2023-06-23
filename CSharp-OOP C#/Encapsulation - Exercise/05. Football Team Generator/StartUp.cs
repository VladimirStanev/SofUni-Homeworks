﻿namespace _05._Football_Team_Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        private static List<Team> teamList;
        static void Main(string[] args)
        {
            static void RunEngine()
            {
                teamList = new List<Team>();

                RunEngine();
            }
            
            string command;
            while((command = Console.ReadLine()) != "END")
            {
                string[] cmdArgs = command.Split(';');
                string cmdType = cmdArgs[0];
                string teamName = cmdArgs[1];

                if(cmdType == "Team")
                {
                    AddNewTeam(teamName);
                }
                else if(cmdType == "Add")
                {
                    AddPlayerToTeam(teamName, cmdArgs);
                }
                else if (cmdType == "Remove")
                {
                    string playerName = cmdArgs[2];

                    RemovePlayerFromTeam(teamName, playerName);
                }
                else if (cmdType == "Rating")
                {
                    Team teamToRate = teamList.FirstOrDefault(t => t.Name == teamName);
                    if(teamToRate == null)
                    {
                        throw new InvalidOperationException(string.Format(
                            ExceptionMessages.TeamDoesntExist, teamName));
                    }
                    Console.WriteLine(teamToRate);
                }
            }
        }
        static void AddNewTeam(string teamName)
        {
            Team newTeam = new Team(teamName);
            teamList.Add(newTeam);
        }
        static void AddPlayerToTeam(string teamName, string[] cmdArgs)
        {
            Team joiningTeam = teamList.FirstOrDefault(t => t.Name == teamName);
            if (joiningTeam == null)
            {
                throw new InvalidOperationException(string.Format(
                    ExceptionMessages.TeamDoesntExist, teamName));
            }
            Player joiningPlayer = CreateNewPlayer(cmdArgs);
            joiningTeam.AddPlayer(joiningPlayer);
        }

        static Player CreateNewPlayer(string[] cmdArgs)
        {
            string playerName = cmdArgs[2];
            int endurance = int.Parse(cmdArgs[3]);
            int sprint = int.Parse(cmdArgs[4]);
            int dribble = int.Parse(cmdArgs[5]);
            int passing = int.Parse(cmdArgs[6]);
            int shooting = int.Parse(cmdArgs[7]);

            Player newPlayer = new Player(playerName, endurance, sprint, dribble, passing, shooting);
            return newPlayer;
        }

        static void RemovePlayerFromTeam(string teamName, string playerName)
        {
            Team removingTeam = teamList.FirstOrDefault(t => t.Name == teamName);
            if (removingTeam == null)
            {
                throw new InvalidOperationException(string.Format(
                    ExceptionMessages.TeamDoesntExist, teamName));
            }
            removingTeam.RemovePlayer(playerName);
        }
        static void RateTeam(string teamName, string playerName)
        {
            Team removingTeam = teamList.FirstOrDefault(t => t.Name == teamName);
            if (removingTeam == null)
            {
                throw new InvalidOperationException(string.Format(
                    ExceptionMessages.TeamDoesntExist, teamName));
            }
            removingTeam.RemovePlayer(playerName);
        }
    }
}
