<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">

  <xsl:output method="text" indent="yes" />

  <xsl:param name="table" />

  <xsl:template match="root"><![CDATA[<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">

<xsl:output
   doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN"
   doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"
   method="html" indent="yes" />

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
       
       ]]><xsl:for-each select="table[@id = $table]"><![CDATA[ 
       
       <form method="post" enctype="multipart/form-data">
            <input type="hidden" name="state" value="send"/>
            
            <table class="fields" width="100%" border="0" cellpadding="2" cellspacing="5">
              <col width="15%"/>
              <col width="60%"/>
              <col width="25%"/>
              
              <tr>
                  <td></td>
                  <td><h1>]]><xsl:value-of select="@name"/><![CDATA[</h1></td>
                  <td></td>
              </tr>
              
              ]]><xsl:for-each select="field"><![CDATA[
              
              <tr>
                  <td class="left_col">]]><xsl:value-of select="@name"/><![CDATA[:</td>
                  <td>
                     <input class="input_text" type="text" name="field_]]><xsl:value-of select="@code"/><![CDATA[" />
                  </td>
                  <td>]]><xsl:value-of select="@type"/><![CDATA[</td>
              </tr>
              
              ]]></xsl:for-each><![CDATA[
              
              <tr>
                  <td></td>
                  <td>
                     <input type="submit" value="Додати" />
                  </td>
                  <td></td>
              </tr>
              
           </table>
           
       </form>
       
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