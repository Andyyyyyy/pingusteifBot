using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;

namespace TwitchBot
{
	class Program
	{
		static void Main(string[] args)
		{
			TwitchChatBot bot = new TwitchChatBot();

			bot.Connect();

			Console.ReadLine();

			bot.Disconnect();
		}
	}
}
