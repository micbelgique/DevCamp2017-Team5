import { FamiDeskWebPage } from './app.po';

describe('fami-desk-web App', () => {
  let page: FamiDeskWebPage;

  beforeEach(() => {
    page = new FamiDeskWebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
