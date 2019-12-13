<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="text" indent="yes" />

  <xsl:param name="table" />

  <xsl:template match="shema"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
<xsl:output method="html" indent="yes" />

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

        <h2>Список: ]]><xsl:value-of select="@name"/><![CDATA[</h2>
        <p><a href="../">Головна</a> | <a href="item">Додати</a></p>
        
        <table class="list" width="100%" cellpadding="2" cellspacing="2">
           <tr>
              ]]><xsl:for-each select="field"><![CDATA[
              <td class="header">]]><xsl:value-of select="@name"/><![CDATA[</td>
              ]]></xsl:for-each><![CDATA[
           </tr>
           <xsl:for-each select="list/row">
              <tr>]]>
                  <xsl:for-each select="field">
                    <![CDATA[<td class="row">]]>
                      <xsl:choose>
                        <xsl:when test="@code = 'ID'">
                             <![CDATA[<xsl:value-of select="]]><xsl:value-of select="@code"/><![CDATA["/>]]>
                             <xsl:for-each select="../subtable">
                                  <xsl:if test="position() = 1"> - </xsl:if>
                                  <xsl:if test="position() > 1"> | </xsl:if>
                                  <![CDATA[<a href="../]]><xsl:value-of select="@id"/><![CDATA[/?e={ID}">]]><xsl:value-of select="@name"/><![CDATA[</a>]]>
                             </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                             <![CDATA[<xsl:value-of select="]]><xsl:value-of select="@code"/><![CDATA["/>]]>
                        </xsl:otherwise>
                      </xsl:choose>
                    <![CDATA[</td>]]>
                  </xsl:for-each>
                  <![CDATA[
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