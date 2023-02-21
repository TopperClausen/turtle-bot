const { Client, Collection, GatewayIntentBits } = require('discord.js');
const { loadCommands, loadSlashCommands } = require('./command_handler');
const { loadEvents } = require('./events/eventHandler');

require('dotenv').config();
const { TOKEN } = process.env;

const client = new Client({ intents: [GatewayIntentBits.Guilds, GatewayIntentBits.GuildMessages, GatewayIntentBits.MessageContent] });

loadEvents(client);
loadCommands(client);
loadSlashCommands(client);

client.login(TOKEN);
