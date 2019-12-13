<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="text" indent="yes" />

  <xsl:param name="table" />
  
  <xsl:template match="shema"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
<xsl:output method="html" indent="yes" />

   <xsl:param name="parent_id" />

   <xsl:template match="root">
 
   <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />        
        <link rel="stylesheet" href="/IMAGE/css/style.css" type="text/css" />
        <title>
          <xsl:text>Образи</xsl:text>
        </title>
      </head>
      <body>
       
       ]]><xsl:for-each select="image/table[@id=$table]"><![CDATA[ 

        <h2>]]><xsl:value-of select="@name"/><![CDATA[</h2>
        <p><a href="../">Головна</a> | <a href="item/?e={$parent_id}">Додати</a></p>
        
        <table class="list" width="100%" cellpadding="2" cellspacing="2">
           <tr>
              ]]><xsl:for-each select="field"><![CDATA[
              <td class="header">]]><xsl:value-of select="@name"/><![CDATA[</td>
              ]]></xsl:for-each><![CDATA[
           </tr>
           <xsl:for-each select="list/row">
              <tr>
                   ]]><xsl:for-each select="field"><![CDATA[
                   <td class="row"><xsl:value-of select="]]><xsl:value-of select="@code"/><![CDATA["/></td>
                   ]]></xsl:for-each><![CDATA[
              </tr>
          </xsl:for-each>
       </table>
       
       ]]></xsl:for-each><![CDATA[
       
      </body>
    </html>

   </xsl:template>
</xsl:stylesheet>
    ]]>
  
  </xsl:template>
</xsl:stylesheet>