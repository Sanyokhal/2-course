const CronJob = require('cron').CronJob;
const {getUsers} = require("../database.js");

function startUserLoginReminderJob() {
    const job = new CronJob(
        '0 30 6 * * 1', // At 06:30 on Monday
        async () => {

            let users = await getUsers();

            users.forEach(user => {
                console.log(`[usersLogReminder.job] Todo send reminder email for: ${user.username}`);
            });

        },
    );

    job.start();
}

module.exports = startUserLoginReminderJob;