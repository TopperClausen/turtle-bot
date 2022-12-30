const { REST, Routes } = require('discord.js');
const { Events } = require('discord.js');
const fs = require('fs');

const register = (client, files) => {
  let commands = []

  for(const file of files) {
    const command = require('./slashCommands/' + file);
    commands.push(command.data.toJSON());

    const rest = new REST({ version: '10' }).setToken(process.env.TOKEN);

    (async () => {
      try {
        console.log('Started refreshing application (/) commands.');

        await rest.put(
          Routes.applicationGuildCommands(process.env.CLIENT_ID, process.env.GUILD_ID),
          { body: commands },
        );

        console.log('Successfully reloaded application (/) commands.');
      } catch (error) {
        console.error(error);
      }
    })()
  }
}

module.exports = {
  loadCommands: (client) => {
    const commandFiles = fs.readdirSync('./commands').filter(file => file.endsWith('.js'));
    const prefix = '!';

    client.on('messageCreate', message => {
      if(!message.content.startsWith(prefix) || message.author.bot) return;

      const messageArray = message.content.split(" ");
      const command = messageArray[0].replace('!', '').toLowerCase();
      const args = messageArray.slice(1);

      // this line dynamically loads the command file and executes it
      require('./commands/' + commandFiles.find(file => file.replace('.js', '') === command)).execute(message, args);
    })
  },

  loadSlashCommands: (client) => {
    const commandFiles = fs.readdirSync('./slashCommands').filter(file => file.endsWith('.js'));

    register(client, commandFiles);
    client.on(Events.InteractionCreate, async interaction => {
      if (!interaction.isChatInputCommand()) return;

      commandFiles.forEach(file => {
        const command = require('./slashCommands/' + interaction.commandName);
        command.execute(interaction);
      })
    })
  },
}
