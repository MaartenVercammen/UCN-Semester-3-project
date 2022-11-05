module.exports = {
  extends: ['airbnb', 'airbnb-typescript'],
  parser: '@typescript-eslint/parser',
  rules: {
    // Disable unless prop-types are used.
    'react/prop-types': 'off',
    // see axios.ts
    'prefer-promise-reject-errors': 'off',
    // TODO: enhance accessibility
    'jsx-a11y/anchor-is-valid': 'off',
    'jsx-a11y/anchor-has-content': 'off',
    'jsx-a11y/label-has-associated-control': 'off',
    'jsx-a11y/click-events-have-key-events': 'off',
    'jsx-a11y/control-has-associated-label': 'off',
    'jsx-a11y/no-static-element-interactions': 'off',
  },
  env: {
    browser: true,
  },
  parserOptions: {
    project: './tsconfig.eslint.json'
  }
};

