<div id="top"></div>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <!-- <a href="https://github.com/Ark667/NetBom">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

<h1 align="center">NetBom</h1>

  <p align="center">
    Simple .Net5 application for moving text literals to resource file.
    <br />
    <a href="https://github.com/Ark667/NetBom"><strong>Explore the docs »</strong></a>
    <br />    
    <a href="https://github.com/Ark667/NetBom/issues">Report Bug</a>
    ·
    <a href="https://github.com/Ark667/NetBom/issues">Request Feature</a>
    <br />    
  </p>
</div>


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#usage">Usage</a></li>
    <!-- <li><a href="#roadmap">Roadmap</a></li> -->
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <!-- <li><a href="#acknowledgments">Acknowledgments</a></li> -->
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<!-- [![Product Name Screen Shot][product-screenshot]](https://example.com) -->

The project starts with the idea of helping to internationalize a Web application. All the hardcoded literals must
be placed in a resource file, and all this work could be made easily by a tool.

In the context fo Visual Studio and .Net way of making this internatinalization process, this tool automatically extract text literals from <strong>cshtml</strong> files to a resource <strong>resx</strong> file.
A Razor reference will be written in place, referencing the value on the resource file.

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [.Net5](https://dotnet.microsoft.com/download/dotnet/5.0)
* [CommandLineParser](https://github.com/commandlineparser/commandline)
* [ResxResourceReader.NetStandard]()

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

You can execute current release with Docker.

```pws

```

You can also clone the repo and build it yourself.

1. Clone the repo

   ```pws
   git clone https://github.com/Ark667/NetBom.git
   ```

2. Build application

   ```pws
   dotnet build .\NetBom\NetBom.sln
   ```

3. Execute help option

   ```pws
   .\NetBom\NetBom\bin\Debug\net7.0\netbom.exe --help
   ```

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- USAGE EXAMPLES -->
## Usage

Basic usage is pretty straightforward. Just call with required paths. By default the tool will create new files
with modifications, use -r argument to make replacements.

```pws

```

Can also be executed from Docker container. First build the image.

```pws
```

Then execute from container. The mapped volume will allow
the command to access local files to make the extractions.

```pws

```

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- LICENSE -->
## License

Feel free to use the code in this repository as it is under MIT license. See `LICENSE.txt` for more information.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I2I16OYC5)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- CONTACT -->
## Contact

Aingeru Medrano - [@AingeruBlack](https://twitter.com/AingeruBlack) <!-- - email@email_client.com -->

Project Link: [https://github.com/Ark667/NetBom](https://github.com/Ark667/NetBom)

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- ACKNOWLEDGMENTS
## Acknowledgments

* []()
* []()
* []()

<p align="right">(<a href="#top">back to top</a>)</p>
 -->


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/Ark667/NetBom.svg?style=for-the-badge
[contributors-url]: https://github.com/Ark667/NetBom/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Ark667/NetBom.svg?style=for-the-badge
[forks-url]: https://github.com/Ark667/NetBom/network/members
[stars-shield]: https://img.shields.io/github/stars/Ark667/NetBom.svg?style=for-the-badge
[stars-url]: https://github.com/Ark667/NetBom/stargazers
[issues-shield]: https://img.shields.io/github/issues/Ark667/NetBom.svg?style=for-the-badge
[issues-url]: https://github.com/Ark667/NetBom/issues
[license-shield]: https://img.shields.io/github/license/Ark667/NetBom.svg?style=for-the-badge
[license-url]: https://github.com/Ark667/NetBom/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/aingeru/
[product-screenshot]: images/screenshot.png
