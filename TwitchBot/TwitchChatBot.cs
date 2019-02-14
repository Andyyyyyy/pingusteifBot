using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;
using TwitchLib.Services;
using TwitchLib;
using TwitchLib.Events.Services.FollowerService;

namespace TwitchBot
{
	internal class TwitchChatBot
	{
		readonly ConnectionCredentials credentials = new ConnectionCredentials(TwitchInfo.BotUsername, TwitchInfo.BotToken);

		TwitchLib.TwitchClient client;

		FollowerService fs = new FollowerService(5, 2);

		List<string> commands = new List<string>();
		int failcounter = 0;
		int monsterfailcounter = 0;
		int spinnercounter = 0;
		Random rand = new Random();
		string[] xd = { "XDXDXDXDXDXD", "xdxd", "XdDXDXdDXDDXD", "xxDD", "xdddddddddddddd", "XDXXdxdXDDxDXd", "XD", "xdxdddxdxdxdx", "xD", "Xd", "XXDDD", "XDdxXDDDXD"};
		System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\notif.wav");

		public TwitchChatBot()
		{
			commands.Add("!nicememe");
			commands.Add("!yt");
			commands.Add("!discord");
			commands.Add("!donate");
			commands.Add("!lowQuality");
			commands.Add("!fleisch");
			commands.Add("!wat");
			commands.Add("!pingu");
			commands.Add("!steif");
			commands.Add("!ping");
			commands.Add("!ts");
			commands.Add("!fail");
			commands.Add("!monsterfail");
			//commands.Add("!spin");
			commands.Add("!hackfleischhassenderzerhacker");
			commands.Add("!lotto");
			commands.Add("!giveaway");
			TwitchAPI.Settings.AccessToken = TwitchInfo.BotToken;
			fs.SetChannelByChannelId(TwitchAPI.Users.v5.GetUserByNameAsync("pingusteif").Result.Matches[0].Id);
		}

		internal void Connect()
		{
			Console.WriteLine("Connecting");

			client = new TwitchLib.TwitchClient(credentials, TwitchInfo.Channelname, logging: true);

			client.OnLog += Client_OnLog;
			client.OnConnectionError += Client_OnConnectionError;
			client.OnMessageReceived += Client_OnMessageReceived;
			fs.OnServiceStarted += followService_OnServiceStarted;
			fs.OnNewFollowersDetected += followService_OnNewFollowersDetected;

			client.Connect();
			fs.StartService();

			Console.WriteLine(fs.ChannelData);

		}

		private void followService_OnServiceStarted(object sender, OnServiceStartedArgs e)
		{
			Console.WriteLine("FollowService started");
		}

		private void followService_OnNewFollowersDetected(object sender, OnNewFollowersDetectedArgs e)
		{
			Console.WriteLine("New Follower");
			foreach (string s in e.NewFollowers)
			{
				client.SendMessage(s + " is now a Penguin 🐧!");
			}
		}

		private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
		{
			string m = e.ChatMessage.Message.ToLower();
			if (m == "!yt")
			{
				client.SendMessage("Subscribe to PinguSteif on YouTube! https://youtube.com/pingusteif");
			}
			else if (m == "!lowquality")
			{
				client.SendMessage("mimimimimimimimi​");
			}
			else if (m == "!discord")
			{
				client.SendMessage("PinguSteif Discord​: https://discord.gg/CZCRhEQ");
			}
			else if (m == "!donate")
			{
				client.SendMessage("That'd be​ very cash money of you: https://streamlabs.com/pingusteif");
			}
			else if (m == "!wat")
			{
				client.SendMessage("Wat?​");
			}
			else if (m == "!nicememe")
			{
				client.SendMessage("𝙉𝙞𝙘𝙚 𝙈𝙚𝙢𝙚​ 👌​");
			}
			else if (m == "!commands")
			{
				string commandsString = "";

				foreach (string cmd in commands)
				{
					if (cmd == commands.Last())
					{
						commandsString += "and " + cmd + ".";
					}
					else
					{
						commandsString += cmd + ", ";
					}
				}

				client.SendMessage("Available commands:​ " + commandsString + " If you have an idea for a new command, feel free to tell me");
			}
			else if (m == "!fleisch")
			{
				client.SendMessage("Ein Stück Fleisch für " + e.ChatMessage.DisplayName + " 🍖 NomNom");
			}
			else if (m == "!ping")
			{
				client.SendMessage("Pong!");
			}
			else if (m == "!pingu")
			{
				client.SendMessage(e.ChatMessage.DisplayName + " ist ein Pinguin 🐧");
			}
			else if (m == "!steif")
			{
				client.SendMessage("🍆 ( ͡° ͜ʖ ͡°)");
			}
			else if (m == "!ts")
			{
				client.SendMessage("Join our Teamspeak 62.75.137.246:1111");
			}
			else if (m == "!fail")
			{
				failcounter++;
				client.SendMessage("ＦＡＩＬ😞👎🎺🚫 No. " + failcounter);
			}
			else if (m == "!monsterfail")
			{
				monsterfailcounter++;
				client.SendMessage("MOMOMOMO 𝐌𝐎𝐍𝐒𝐓𝐄𝐑𝐅𝐀𝐈𝐋 (fail) TOTAL MOMOMO𝐌𝐎𝐍𝐒𝐓𝐄𝐑𝐅𝐀𝐈𝐋S: " + monsterfailcounter);
			}
			else if (m == "!spin")
			{
				spinnercounter++;
				client.SendMessage("THE SPINNER STOPPED! TOTAL STOPS: " + spinnercounter);
			}
			else if (m == "!hackfleischhassenderzerhacker")
			{
				client.SendMessage("@" + e.ChatMessage.DisplayName + " er zerhackt dich! 🔪");
			}
			else if (m == "!giveaway")
			{
				client.SendMessage("Willste Isolierband?");
			}
			else if(m == "!lotto")
			{
				client.SendMessage("Too bad. You lost the lottery ¯\\_(ツ)_/¯ ");
			}
			if (m.Contains("xd"))
			{
				int randomPick = rand.Next(0, xd.Length - 1);
				client.SendMessage(xd[randomPick]);
			}
			
			player.Play();
		}

		private void Client_OnLog(object sender, OnLogArgs e)
		{
			Console.WriteLine(e.Data);
		}

		private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
		{
			Console.WriteLine($"Error: {e.Error}");
		}

		internal void Disconnect()
		{
			Console.WriteLine("Disconnecting");
		}
	}
}