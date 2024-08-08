# slatemoney.kb
Slate Money Knowledge Base  - online Help and Product Guide

SLATE KNOWLEDGE BASE PRE-PROCESSOR

The base collection of html/css assets from

Guidex - Online Documentation HTML Template + Help Desk + Knowledge Base + Forum

Purchased from envao market place

From <https://themeforest.net/order_confirmation/695d8d7f-58c7-4381-b072-b8a069c6bd97> 


====== Why do we need a pre-processor? ==========
Because the common side bar must be generated for each page! Doing this manually high error prone basically *not possible*. 
Attempting the js trick of html injection only partially works. Some of the page behaviours don't work with this method

The pre-processor actually generates the common side bar HTML and injects into each page.

Another useful artefact of this approach is that editing page content is clearer without all the header/side bar confusion. Allows focus on page content.


====== How it works ==========

KB is made of a collection of individual pages held in /assets/pages

*index-maincontent.html
*licence-maincontent.html
etc.

* Maintain all these pages under source control
* Edit the pages (in /assets/pages) using VS Code. Open in browser for live WYSIWYG
* Do not edit the copied content pages in bin/debug !

====== Manifest  ==========

* Collate all pages to be pre-processed in manifest.xml. 
* Do not change the location of manifest.xml

e.g.

  <Page src="index-maincontent.html" target="index.html" title="SlateMoney - Knowledge Base"   openinbrowser="true">
    <Topic href="#gx_intro" legend="Introduction"/>
    <Topic href="#gx_signup" legend="Registration"/>
    <Topic href="#gx_installation" legend="Installation"/>
  </Page>

* Pages may have side topics (helpful in-page navigation links) which appear on the RHS of page content. 
* Reference each desired topic anchor with a <Topic> element. Ensure the anchor is present in the page
* Side topics allow more granular navigation than you would want to put in the main (left hand) Sidebar

* Declare the spec of the common side bar with  <Sidebar> element
* Note the attributes of each <Entry> element

 <Entry class="dropitem"  legend="Getting Started" icon="ri-dashboard-3-line" >
      <Navlink legend="Introduction" href="index.html#gx_intro"/>
      <Navlink legend="User sign-up" href="index.html#gx_signup"/>
      <Navlink legend="Installation" href="index.html#gx_installation"/>
    </Entry>


* icon identifiers refer to the remix icon collection (https://remixicon.com/)


====== Pre-processor ==========

Loads and parses manifest.xml

Generates the common side bar HTML.

For each <Page> element

	* extracts the main-content of each page and injects that into a page template (see \templates\page-template.html)
	* injection adds the page topics and common side bar to each page
	* opens in browser the first page found where openinbrowser attrib is "true"



====== Related Tools ==========
VS Code