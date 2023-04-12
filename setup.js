const readline = require('readline');
const fs = require('fs');

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

const questions = [
  'Enter the company name: ',
//   'Enter the path to the company logo: ',
];

let answers = {};

const askQuestion = (index) => {
  if (index >= questions.length) {
    // All questions have been answered
    // Update your project's configuration here
    // For example, if you have a project.json file that specifies the server title:
    const projectConfig = require('./project.json');
    projectConfig.serverTitle = answers.companyName;
    const apiUrl="https://"+process.env.CODESPACE_NAME+"/api";
    projectConfig.apiURL=apiUrl;
    // projectConfig.logoPath = answers.companyLogo;
    fs.writeFileSync('./project.json', JSON.stringify(projectConfig, null, 2));

    // Similarly, update the frontend code to display the company name on the website

    rl.close();
  } else {
    rl.question(questions[index], (answer) => {
      switch (index) {
        case 0:
          answers.companyName = answer;
          break;
/*         case 1:
          answers.companyLogo = answer;
          break; */
      }
      askQuestion(index + 1);
    });
  }
};

askQuestion(0);
