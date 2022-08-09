# API Frontend (frontend)

API Frontend of the project, using VueJS, Vite and Quasar.

## Install the dependencies
```bash
yarn
# or
npm install
```

### Start the app in development mode (hot-code reloading, error reporting, etc.)
```bash
quasar dev
```


### Lint the files (Not currently needed until scaffolding is complete)
```bash
yarn lint
# or
npm run lint
```



### Build the app for production (Ignore for now)
```bash
quasar build
```

### Working with code
The main view is in the layouts directory, the pages we add will be added as Vue pages in the `pages` folder and the components to be used in those pages (e.g. Forms for login, charts for dashboard etc) will be in the `components` folder.

### Naming Conventions
The .Vue File names are Pascal-cased, and the components will be placed in their typed directory. For example, all components related to Tables will go in `components/tables/` , and components using Cards will go in `components/cards/` and so on.
Any component not falling into any specific category will be placed directly in the `components/` directory.
