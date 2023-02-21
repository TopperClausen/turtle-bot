const { SlashCommandBuilder, SlashCommandSubcommandGroupBuilder } = require('@discordjs/builders');

module.exports = {
  data: new SlashCommandBuilder()
    .setName('q')
    .setDescription('Replies with a random quote'),
    async execute(interaction, client) {
      const channel = interaction.member.guild.channels.cache.find(channel => channel.name === 'quotes');
      const messages = [...(await channel.messages.fetch({ limit: 100 }))];
      const number = Math.floor(Math.random() * (messages.length - 0 + 1) + 0)
      await interaction.reply({ content: messages[number][1].content });
    }
}