using Matchmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Matchmaker
{
    public class ActionContainer
    {
        public Actions.Action Action { get; private set; }
        public string[] Command { get; private set; }

        public ActionContainer(Actions.Action action)
        {
            Action = action;
            Command = action.Name.Split(" ");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Type actionType = typeof(Actions.Action);
            actions = actionType.Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(actionType))
                .Select(type => new ActionContainer((Actions.Action)Activator.CreateInstance(type)))
                .ToArray();

            while (true)
            {
                Console.Write(">> ");
                string[] input = Console.ReadLine().ToLower().Trim().Split(" ");

                if (input.Length == 0 || input[0] == "")
                {
                    Console.WriteLine("Got no input");
                    Console.WriteLine("Use 'exit' to exit");
                    continue;
                }

                if (input[0] == "help")
                {
                    Console.WriteLine("Commands:");
                    PrintActions(input.Length > 1
                        ? MatchActionNamesByArray(input.Skip(1).ToArray())
                        : actions);

                    continue;
                }

                if (input[0] == "exit")
                {
                    break;
                }

                ActionContainer[] inputActions = MatchArrayByActionNames(input);

                if (inputActions.Length == 0)
                {
                    Console.WriteLine("Command not found");

                    inputActions = MatchActionNamesByArray(input);
                    if (inputActions.Length > 0)
                    {
                        Console.WriteLine("Possible commands: ");
                        PrintActions(inputActions);
                    }

                    continue;
                }

                if (inputActions.Length > 1)
                {
                    Console.WriteLine("Got multiple possible actions");
                    PrintActions(inputActions);
                    continue;
                }

                if (!inputActions[0].Action.Call(input.Skip(inputActions[0].Command.Length).ToArray()))
                {
                    Console.WriteLine("Command failed, Usage:");
                    PrintActions(inputActions);
                    continue;
                }
            }
        }

        private static ActionContainer[] actions;

        // used when u have less arguments than action names
        private static ActionContainer[] MatchActionNamesByArray(string[] arguments)
        {
            return actions.Where((action) =>
            {
                if (action.Command.Length < arguments.Length)
                    return false;

                for (int i = 0; i < arguments.Length; ++i)
                    if (!action.Command[i].StartsWith(arguments[i]))
                    {
                        return false;
                    }

                return true;
            }).ToArray();
        }

        // used when u have more arguments than action names
        private static ActionContainer[] MatchArrayByActionNames(string[] arguments)
        {
            return actions.Where((action) =>
            {
                if (action.Command.Length > arguments.Length)
                    return false;

                for (int i = 0; i < action.Command.Length; ++i)
                    if (!action.Command[i].StartsWith(arguments[i]))
                    {
                        return false;
                    }

                return true;
            }).ToArray();
        }

        private static void PrintActions(ActionContainer[] actions)
        {
            int maxUsageLength = actions.Max((a) => a.Action.Usage.Length) + 3;
            
            foreach (ActionContainer action in actions.OrderBy((a) => a.Action.Name))
            {
                Console.WriteLine($"=> {action.Action.Usage}{new string(' ', maxUsageLength - action.Action.Usage.Length)}");
                Console.WriteLine($"     {action.Action.Description}");
            }
        }
    }
}
