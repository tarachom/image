<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="text" indent="no" />
  
  <!-- ІД таблиці -->
  <xsl:param name="table" />
  
  <!-- ІД родітельської таблиці -->
  <xsl:param name="parent_table_id" />

  <xsl:template match="shema">
    <![CDATA[<?php
  
include("../include/include.php");

//Масив запитів
$sqlArray = array();

//ХМЛ дані
$xmlData = "";

//Список параметрів
$xslParams = array();

]]>

    <xsl:for-each select="image/table[@id=$table]">

      //Родітельський запис
      $parent_id = isset($_GET["e"]) ? (int)$_GET["e"] : 0;
      $xslParams["parent_id"] = $parent_id;
       
      if ($parent_id != 0) {
         
          //@code != 'ID' and @code != 'PARENTID' and @code != 'OBJECTID'

          $sqlArray["list"] = "
          SELECT
          <xsl:for-each select="field">
            <xsl:if test="position() > 1">
              <xsl:text>,</xsl:text>
            </xsl:if>
            <xsl:choose>
              <xsl:when test="@type = 'link'">
                <xsl:text>`</xsl:text>
                <xsl:value-of select="@table_link"/>
                <xsl:text>`.`NAME` as `</xsl:text>
                <xsl:value-of select="@code"/>
                <xsl:text>`</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>`</xsl:text>
                <xsl:value-of select="../@code"/>
                <xsl:text>`.`</xsl:text>
                <xsl:value-of select="@code"/>
                <xsl:text>`</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:for-each>
          FROM
          <xsl:text>`</xsl:text>
          <xsl:value-of select="@code"/>
          <xsl:text>`</xsl:text>
          <xsl:for-each select="field">
            <xsl:if test="@type = 'link'">
              <xsl:text> LEFT JOIN `</xsl:text>
              <xsl:value-of select="@table_link"/>
              <xsl:text>` ON `</xsl:text>
              <xsl:value-of select="@table_link"/>
              <xsl:text>`.`ID` = `</xsl:text>
              <xsl:value-of select="../@code"/>
              <xsl:text>`.`</xsl:text>
              <xsl:value-of select="@code"/>`
            </xsl:if>
          </xsl:for-each>
          WHERE `PARENTID` = " . $parent_id;
      }
    </xsl:for-each>

    <![CDATA[

$xmlData = GenerateXML($sqlArray, $xmlData);
echo GenerateHTML(false, $xmlData, "index.xsl", $xslParams);

?>]]>

  </xsl:template>
</xsl:stylesheet>