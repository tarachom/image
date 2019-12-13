<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
  <xsl:output method="text" indent="no" />

  <xsl:param name="table" />

  <xsl:template match="shema">
    <![CDATA[<?php
  
include("../include/include.php");

//Масив запитів
$sqlArray = array();

//ХМЛ дані
$xmlData = "";

]]>

    <xsl:for-each select="image/table[@id=$table]">

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
      ";

    </xsl:for-each>

    <![CDATA[

$xmlData = GenerateXML($sqlArray, $xmlData);
echo GenerateHTML(false, $xmlData, "index.xsl");

?>]]>

  </xsl:template>
</xsl:stylesheet>