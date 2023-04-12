const { execSync } = require("child_process");

// Replace existing wwwroot directory with the new one
execSync("mv -f wwwroot ../backend/");
